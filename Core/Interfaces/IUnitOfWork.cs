﻿namespace Core.Interfaces;

public interface IUnitOfWork:IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    INumeroSolicitudRepository NumeroSolicitudRepository { get; }
    ISolicitudRepository SolicitudRepository { get; }
    IUsuarioRepository UsuarioRepository { get; }
}
