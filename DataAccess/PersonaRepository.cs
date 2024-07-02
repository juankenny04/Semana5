using Semana5.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SQLite.SQLite3;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Semana5.DataAccess
{
    public class PersonaRepository
    {
        string dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        private void Init()
        {
            if (conn is not null)
            
                return;
                conn = new(dbPath);
                conn.CreateTable<Persona>();
            
        }

        public PersonaRepository(string path)
        {
            dbPath = path;
        }

        public void AddNewPerson(string nombre)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(nombre))
                
                    throw new Exception("El nombre es requerido");

                    Persona person = new() { Name = nombre };
                    result = conn.Insert(person);

                    StatusMessage = string.Format("Dato añadido correctamente", nombre, result);
                
            }
            catch (Exception e)
            {
                StatusMessage = string.Format("Error al insertar", nombre, result);
            }
        }

        public List<Persona> GetAll()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception e)
            {

                StatusMessage = string.Format("Error al mostrar",e.Message);
            }
            return new List<Persona>();
        }

        public void DeletePerson(string nombre)
        {
            int result = 0;
            try
            {
                Init();

                var personToDelete = conn.Table<Persona>().FirstOrDefault(p => p.Name == nombre);
                if (personToDelete == null)
                    throw new Exception("Persona no encontrada");

                result = conn.Delete(personToDelete);
                StatusMessage = string.Format("Dato eliminado correctamente", nombre, result);
            }
            catch (Exception e)
            {
                StatusMessage = string.Format("Error al eliminar", nombre, e.Message);
            }
        }

        public void UpdatePerson(string nombre, string nuevoNombre)
        {
            try
            {
                var personToUpdate = conn.Table<Persona>().FirstOrDefault(p => p.Name == nombre);
                if (personToUpdate != null)
                {
                    personToUpdate.Name = nuevoNombre;
                    conn.Update(personToUpdate);
                    StatusMessage = string.Format("Dato actualizado correctamente", nombre);
                }
                else
                {
                    StatusMessage = string.Format("Persona no encontrada", nombre);
                }
            }
            catch (Exception e)
            {
                StatusMessage = string.Format("Error al actualizar", nombre, e.Message);
            }
        }
    }
}
    

