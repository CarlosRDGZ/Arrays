using System;
using EstrcuturasConArreglos;
using Gtk;

public partial class MainWindow : Gtk.Window
{
	Catalog _catalog = new Catalog(15);

	public MainWindow() : base(Gtk.WindowType.Toplevel) => Build();

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

	protected void BtnAddClick(object sender, EventArgs e)
	{
		int x;
		try
		{
			x = int.Parse("error");
		}
		catch(FormatException ex)
		{
			new MessageDialog(
				this,
				DialogFlags.Modal,
				MessageType.Error,
				ButtonsType.Ok,
				"", ex).Show();
		}
	}
}
