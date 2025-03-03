﻿using Amazon.S3.Transfer;
using Amazon.S3;
using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Interfaces;
using Core.Request;
using Core.Response;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class SolicitudService : ISolicitudService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly INumeroSolicitudService _numeroSolicitudService;
    private readonly IUsuarioService _usuarioService;
    string relationsUsers = "Estados_Solicitudes,Tipos_Solicitudes,Usuarios,Usuarios.Tipo_Identificacion";
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly string _bucketRegion;


    public SolicitudService(IUnitOfWork unitOfWork, IMapper mapper, INumeroSolicitudService numeroSolicitudService, IUsuarioService usuarioService, IAmazonS3 s3Client, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _numeroSolicitudService = numeroSolicitudService;
        _usuarioService = usuarioService;
        _s3Client = s3Client;
        _bucketName = configuration["AWSS3BUCKET:BucketName"];
        _bucketRegion = configuration["AWSS3BUCKET:Region"];
    }

    public async Task<SolicitudResponse> Add(SolicitudRequest request, IFormFile file, CancellationToken cancellationToken)
    {
        string nomFile = "";
        Solicitudes entity = _mapper.Map<Solicitudes>(request);

        var usuario = await _usuarioService.GetByEmail(request.Usuario.us_correo) ;

        if(usuario is not null)
        {
            entity.so_us_id = usuario.us_id;
        }
        else
        {
            UsuarioRequest User = _mapper.Map<UsuarioRequest>(request.Usuario);
            var newUser = await _usuarioService.Add(User, cancellationToken);


            if (newUser is not null)
            {
                entity.so_us_id = newUser.us_id;
            }
            else
            {
                return null;
            }
        }

        var numeroSolicitud = await _numeroSolicitudService.Add(cancellationToken);

        entity.so_numero_solicitud = numeroSolicitud.ns_numero;

        //Carga el documento
       if(file is not null && file.Length > 0)
            nomFile = await UploadFile(file);

       if (nomFile != "")
            entity.so_url_image = nomFile;

        await _unitOfWork.SolicitudRepository.Create(entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if(result > 0)
        {
            var entidadNueva = await GetById(entity.so_id);

            var entityResponse = _mapper.Map<SolicitudResponse>(entidadNueva);
            return entityResponse;
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.SolicitudRepository.Delete(id, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<IEnumerable<SolicitudResponse>> GetAll()
    {
        IEnumerable<Solicitudes?> data = await _unitOfWork.SolicitudRepository.ReadAll(includeProperties: relationsUsers);
        IEnumerable<SolicitudResponse> response = _mapper.Map<IEnumerable<SolicitudResponse>>(data);
        return response;
    }

    public async Task<SolicitudResponse> GetById(int id)
    {
        Solicitudes? entity = await _unitOfWork.SolicitudRepository.ReadById(x => x.so_id.Equals(id),
                                                                            includeProperties: relationsUsers);
        SolicitudResponse response = _mapper.Map<SolicitudResponse>(entity);
        return response;
    }

    public async Task<bool> Update(int id, SolicitudRequest request, CancellationToken cancellationToken)
    {
        Solicitudes entity = _mapper.Map<Solicitudes>(request);
        await _unitOfWork.SolicitudRepository.Update(id, entity, cancellationToken);
        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<IEnumerable<SolicitudResponse>> GetByNumber(string number)
    {
        IEnumerable<Solicitudes?> entity = await _unitOfWork.SolicitudRepository.ReadAll(x => x.so_numero_solicitud.Contains(number),
                                                                            includeProperties: relationsUsers);
        IEnumerable<SolicitudResponse> response = _mapper.Map<IEnumerable<SolicitudResponse>>(entity);
        return response;
    }
    public async Task<IEnumerable<SolicitudResponse>> GetByEmail(string Email)
    {
        var usuario = await _usuarioService.GetByEmail(Email);

        if (usuario is null)
            return null;

        IEnumerable<Solicitudes?> entity = await _unitOfWork.SolicitudRepository.ReadAll(x => x.so_us_id.Equals(usuario.us_id),
                                                                            includeProperties: relationsUsers);
        IEnumerable<SolicitudResponse> response = _mapper.Map <IEnumerable<SolicitudResponse>>(entity);
        return response;
    }


    private async Task<string> UploadFile(IFormFile file)
    {
        
        try
        {
            // Generar un nombre único para el archivo (se puede usar un GUID o cualquier otra lógica)
            var fileKey = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Subir el archivo a S3
            var fileTransferUtility = new TransferUtility(_s3Client);
            using (var stream = file.OpenReadStream())
            {
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    BucketName = _bucketName,
                    Key = fileKey,
                    InputStream = stream,
                    ContentType = file.ContentType
                };

                await fileTransferUtility.UploadAsync(uploadRequest);
            }

            // Obtener la URL pública del archivo
            var fileUrl = $"https://{_bucketName}.s3.{_bucketRegion}.amazonaws.com/{fileKey}";

            // Retornar la URL del archivo cargado
            return fileUrl;
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }
}
