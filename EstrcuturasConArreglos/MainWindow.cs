using System;
using EstrcuturasConArreglos;
using Gtk;

public partial class MainWindow : Gtk.Window
{
	Inventory _catalog = new Inventory(15);

	public MainWindow() : base(Gtk.WindowType.Toplevel) => Build();

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	protected void BtnAddClick(object sender, EventArgs e)
	{
		if (IsValid(out Product product))
		{
			_catalog.Add(product);
			MessageDialog msgOk = new MessageDialog(
				this,
				DialogFlags.Modal,
				MessageType.Info,
				ButtonsType.Ok,
				"Producto Agregado"
			);
			msgOk.Run();
			msgOk.Destroy();
			ClearProductTxts();
		}
	}

	protected void BtnListClick(object sender, EventArgs e)
	{
		txtList.Buffer.Text = _catalog.List();
	}

	protected void BtnInsertClick(object sender, EventArgs e)
	{
		if (IsValid(out Product product))
		{
			MessageDialog msg = null;
			try
			{
				_catalog.Insert(product, int.Parse(txtPos.Text));
				msg = new MessageDialog(
					this,
					DialogFlags.Modal,
					MessageType.Info,
					ButtonsType.Ok,
					"Producto Insertado en {0}",
					txtPos.Text
				);
				ClearProductTxts();
			}
			catch (Exception ex)
			{
				string error = "ERROR!";
				if (ex is IndexOutOfRangeException)
					error += " Posicion Invalida.";
				msg = new MessageDialog(
					this,
					DialogFlags.Modal,
					MessageType.Error,
					ButtonsType.Close,
					"{0}{1}{2}",
					error,
					Environment.NewLine,
					ex.Message
				);
			}
			finally
			{
				msg.Run();
				msg.Destroy();
			}
		}
	}

	protected void BtnSearchClick(object sender, EventArgs e)
	{
		Product product = _catalog.Search(txtCode.Text);
		if (product != null)
			txtList.Buffer.Text = product.ToString();
		else
		{
			MessageDialog msgError = new MessageDialog(
				this,
				DialogFlags.Modal,
				MessageType.Error,
				ButtonsType.Close,
				"Producto No Econtrado"
			);
			msgError.Run();
			msgError.Destroy();
			txtCode.Text = "";
		}
	}

	protected void BtnDeleteClick(object sender, EventArgs e)
	{
		MessageDialog msg = null;
		try
		{
			_catalog.Delete(txtCode.Text);
			msg = new MessageDialog(
				this,
				DialogFlags.Modal,
				MessageType.Info,
				ButtonsType.Ok,
				"Producto {0} Borrado",
				txtCode.Text
			);
			txtCode.Text = "";
			txtList.Buffer.Text = "";
		}
		catch (Exception ex)
		{
			msg = new MessageDialog(
				this,
				DialogFlags.Modal,
				MessageType.Error,
				ButtonsType.Close,
				"Producto No Borrado{0}{1}",
				Environment.NewLine,
				ex.Message
			);
		}
		finally
		{
			msg.Run();
			msg.Destroy();
		}
	}

	private bool IsValid(out Product product)
	{
		string[] props =
		{
			txtName.Text,
			txtDescription.Text
		};

		for (int i = 0; i < props.Length; i++)
			if (props[i] == "")
			{
				MessageDialog msgError = new MessageDialog(
					this,
					DialogFlags.Modal,
					MessageType.Error,
					ButtonsType.Close,
					"ERROR! {0} Llenar todos los campos",
					Environment.NewLine
				);
				msgError.Run();
				msgError.Destroy();
				product = null;
				return false;
			}

		int[] nProps;
		try
		{
			nProps = new int[]
			{
				int.Parse(txtQuantity.Text),
				int.Parse(txtPrice.Text)
			};
		}
		catch (FormatException)
		{
			MessageDialog msgError = new MessageDialog(
				this,
				DialogFlags.Modal,
				MessageType.Error,
				ButtonsType.Close,
				"ERROR! {0} Cantidad o Costo No Valido",
				Environment.NewLine
			);
			msgError.Run();
			msgError.Destroy();
			product = null;
			return false;
		}

		product = new Product(props[0], props[1], nProps[0], nProps[1]);
		return true;
	}

	private void ClearProductTxts()
	{
		txtName.Text = "";
		txtDescription.Text = "";
		txtQuantity.Text = "";
		txtPrice.Text = "";
	}
}