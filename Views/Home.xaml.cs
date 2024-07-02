using Semana5.Modelos;
using System.Dynamic;

namespace Semana5.Views;

public partial class Home : ContentPage
{
    private string currentName;
	public Home()
	{
		InitializeComponent();
	}

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
       
        App.PersonaRepository.AddNewPerson(txtNombre.Text);
        status.Text = App.PersonaRepository.StatusMessage;
        status.Text = " ";
        txtNombre.Text = string.Empty;

    }

    private void btnListar_Clicked(object sender, EventArgs e)
    {
        

        List<Persona> personas = App.PersonaRepository.GetAll();
        ListaPersona.ItemsSource = personas;
        status.Text = " ";
        txtNombre.Text = string.Empty;
    }

    private  void btnEliminar_Clicked(object sender, EventArgs e)
    {

        string nombre = txtNombre.Text;

        if (!string.IsNullOrEmpty(nombre))
        {
            try
            {
                App.PersonaRepository.DeletePerson(nombre);
                status.Text = "Persona eliminada correctamente";

                // Actualizar la lista
                List<Persona> personas = App.PersonaRepository.GetAll();
                ListaPersona.ItemsSource = personas;

                // Limpiar el campo de entrada
                txtNombre.Text = string.Empty;
            }
            catch (Exception ex)
            {
                status.Text = $"Error al eliminar: {ex.Message}";
            }
        }
        else
        {
            status.Text = "Ingrese el nombre de la persona para eliminar";
        }
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        string nombre = txtNombre.Text;

        if (!string.IsNullOrEmpty(nombre))
        {
            if (currentName == null)
            {
                // Primer paso: ingresar el nombre actual
                currentName = nombre;
                txtNombre.Text = string.Empty;
                status.Text = "Ingrese el nuevo nombre y presione actualizar nuevamente";
            }
            else
            {
                // Segundo paso: ingresar el nuevo nombre
                string nuevoNombre = nombre;

                try
                {
                    App.PersonaRepository.UpdatePerson(currentName, nuevoNombre);
                    status.Text = "Persona actualizada correctamente";

                    // Actualizar la lista
                    List<Persona> personas = App.PersonaRepository.GetAll();
                    ListaPersona.ItemsSource = personas;

                    // Limpiar los campos de entrada
                    txtNombre.Text = string.Empty;
                    currentName = null;
                }
                catch (Exception ex)
                {
                    status.Text = $"Error al actualizar: {ex.Message}";
                }
            }
        }
        else
        {
            status.Text = "Ingrese el nombre actual y luego el nuevo nombre";
        }
    }

}

   
