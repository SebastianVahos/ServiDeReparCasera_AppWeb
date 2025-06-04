var URLBase = "http://serv-reparacaseras.runasp.net/";
jQuery(function () {
    LlenarTablaProveedores();
    let URL = URLBase + "api/Equipos/LlenarCombo";
    LlenarComboXServiciosAuth(URL, "#cboEquipo");
});
function LlenarTablaProveedores() {
    let URL = URLBase + "api/Proveedores/ConsultarTodos";
    LlenarTablaXServiciosAuth(URL, "#tblProveedores");
}

async function Consultar() {
    let Documento = $("#txtDocumento").val();
    let URL = URLBase + "api/Proveedores/Consultar?idProveedor=" + Documento;
    const proveedor = await ConsultarServicioAuth(URL);
    if (proveedor == null || proveedor == undefined) {
        $("#dvMensaje").removeClass("alert alert-success");
        $("#dvMensaje").addClass("alert alert-danger");
        $("#dvMensaje").html("No se pudo realizar la consulta del proveedor");
        $("#txtDocumento").val("");
        $("#txtNombre").val("");
        $("#txtRazon").val("");
        $("#txtDireccion").val("");
        $("#txtTelefono").val("");
        $("#txtWeb").val("");
    }
    else {
        $("#dvMensaje").removeClass("alert alert-danger");
        $("#dvMensaje").addClass("alert alert-success");
        $("#dvMensaje").html("");
        //Consultó el proveedor
        $("#txtDocumento").val(proveedor.IdProveedor);
        $("#txtNombre").val(proveedor.Nombre);
        $("#txtRazon").val(proveedor.RazonSocial);
        $("#txtDireccion").val(proveedor.Direccion);
        $("#txtTelefono").val(proveedor.Telefono);
        $("#txtWeb").val(proveedor.SitioWeb);
    }
}

async function EjecutarComando(Metodo, Funcion) {  
    let CodEqui = $("#cboEquipo").val();
    let vlrUni = $("#txtVlrUni").val();
    let fechaCoti = $("#txtFechaCoti").val();
    let fechaVali = $("#txtFechaVali").val();
    let Operacion = Funcion == 'Insertar' ? "api/Proveedores/Insertar?codEquipo=" + CodEqui + "&vlrUnitario=" + vlrUni + "&fechaCoti=" + fechaCoti + "&fechaVali=" + fechaVali: "api/Proveedores/" + Funcion;
    let URL = URLBase + Operacion;
    const proveedor = new Proveedor($("#txtDocumento").val(), $("#txtNombre").val(), $("#txtRazon").val(),
        $("#txtDireccion").val(), $("#txtTelefono").val(), $("#txtWeb").val());
    const rpta = await EjecutarComandoServicioAuth(Metodo, URL, proveedor);
    LlenarTablaProveedores();
}

class Proveedor {
    constructor(IdProveedor, Nombre, RazonSocial, Direccion, Telefono, SitioWeb) {
        this.IdProveedor = IdProveedor; 
        this.Nombre = Nombre;
        this.RazonSocial = RazonSocial;
        this.Direccion = Direccion;
        this.Telefono = Telefono;
        this.SitioWeb = SitioWeb;
    }
}