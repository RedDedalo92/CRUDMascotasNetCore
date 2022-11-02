namespace BE_CRUDMascotas.Models.Repository
{
    public interface IMascotaRepository
    {
        Task<List<Mascota>> GetListMascotas();
        Task<Mascota> GetMascotaById(int id);
        Task DeleteMascotaById(Mascota mascota);
        Task<Mascota> AddMascota(Mascota mascota);
        Task UpdateMascota(Mascota mascota);
    }
}
