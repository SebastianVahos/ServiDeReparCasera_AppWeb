var URLBase = "https://localhost:44369/";

jQuery(function () {
    let URL = URLBase + "api/TipoTelefonos/LlenarCombo";
    LlenarTablaClientes();
    LlenarComboXServiciosAuth(URL, "#cboTipoTelefono");
});
function LlenarTablaClientes() {
    let URL = URLBase + "api/Clientes/ClientesConTelefonos";
    LlenarTablaXServiciosAuth(URL, "#tblClientes");
}
function LlenarTablaTelefonos() {
    let Documento = $("#txtDocumento").val();
    let URL = URLBase + "api/Telefonos/ListadoTelefonosXCliente?Documento=" + Documento;
    $("#modTelefonosLabel").html("GESTIÓN DE TELÉFONOS - CLIENTE: " + $("#txtNombre").val());
    LlenarTablaXServiciosAuth(URL, "#tblTelefonos");
}
async function EjecutarComandoCliente(Metodo, Funcion) {
    let URL = URLBase + "api/Clientes/" + Funcion;
    const cliente = new Cliente($("#txtDocumento").val(), $("#txtNombre").val(), $("#txtFechaNacimiento").val(),
                                $("#txtDireccion").val(), $("#txtEmail").val());
    const rpta = await EjecutarComandoServicioAuth(Metodo, URL, cliente);
    LlenarTablaClientes();
}
function Editar(Documento, Nombre, FechaNacimiento, Direccion, Email) {
    $("#txtDocumento").val(Documento);
    $("#txtNombre").val(Nombre);
    $("#txtFechaNacimiento").val(FechaNacimiento);
    $("#txtDireccion").val(Direccion);
    $("#txtEmail").val(Email);
}
function EditarTelefono(Codigo, idTipoTelefono, NumeroTelefono) {
    $("#txtCodigo").val(Codigo);
    $("#cboTipoTelefono").val(idTipoTelefono);
    $("#txtNumero").val(NumeroTelefono);
}
async function EjecutarComando(Metodo, Funcion) {
    event.preventDefault();
    let URL = URLBase + "/api/Telefonos/" + Funcion;
    const telefono = new Telefono($("#txtCodigo").val(), $("#txtNumero").val(), $("#txtDocumento").val(), $("#cboTipoTelefono").val());
    await EjecutarComandoServicioAuth(Metodo, URL, telefono);
    LlenarTablaTelefonos();
}
class Telefono {
    constructor(Codigo, Numero, Documento, CodigoTipoTelefono) {
        this.Codigo = Codigo;
        this.Numero = Numero;
        this.Documento = Documento;
        this.CodigoTipoTelefono = CodigoTipoTelefono;
    }
}
class Cliente {
    constructor(Documento, NombreCompleto, FechaNacimiento, Direccion, Correo) {
        this.Documento = Documento;
        this.NombreCompleto = NombreCompleto;
        this.FechaNacimiento = FechaNacimiento;
        this.Direccion = Direccion;
        this.Correo = Correo;
    }
}