var URLBase = "http://serv-reparacaseras.runasp.net/";

jQuery(function () {
    LlenarTablaEquipos();    
});

function LlenarComboImg() {
    let Documento = $("#txtDocumento").val();
    let URL = URLBase + "api/Equipos/LlenarComboImg?codEquipo=" + Documento;
    LlenarComboXServiciosAuth(URL, "#cboImagenes");
}
function LlenarTablaEquipos() {
    let URL = URLBase + "api/Equipos/ConsultarTodos";
    LlenarTablaXServiciosAuth(URL, "#tblEquipos");
}
function LlenarTablaEquipo() {
    let Documento = $("#txtDocumento").val();
    let URL = URLBase + "api/Equipos/ConsultarImagenes?CodigoEquipo=" + Documento;
    $("#modImagenesLabel").html("GESTIÓN DE IMAGENES - EQUIPO: " + $("#txtNombre").val());
    Token = getCookie("token");
    $.ajax({
        
        mode: "cors",
        url: URL,
        method: "GET",
        headers: {
            'Authorization': 'Bearer ' + Token
        },       
        success: function (data) {
            $('#tblImagenes').DataTable().clear().destroy(); // Reinicia tabla

            $('#tblImagenes').DataTable({
                data: data,
                columns: [
                    { data: 'IdImagen' }, 
                    { data: 'Equipo' },                  
                    {
                        data: 'Imagen',
                        render: function (data, type, row) {
                            // Suponiendo que RutaImagen es algo como: /archivos/miimagen.jpg
                            return `<img src="${URLBase}archivos/${data}" width="100" height="100" alt="Imagen del equipo"/>`;
                        }
                    },
                    { data: 'codEquipo' }
                ]
            });
        },
        error: function () {
            $("#dvMensajet").removeClass("alert alert-success");
            $("#dvMensajet").addClass("alert alert-danger");
            $("#dvMensajet").html("No se pudieron cargar las imágenes del equipo.");
        }
    });
}

async function ConsultarEquipo() {
    let Documento = $("#txtDocumento").val();
    let URL = URLBase + "api/Equipos/Consultar?CodigoEquipo=" + Documento;
    const equipo = await ConsultarServicioAuth(URL);
    if (equipo == null || equipo == undefined) {
        $("#dvMensaje").removeClass("alert alert-success");
        $("#dvMensaje").addClass("alert alert-danger");
        $("#dvMensaje").html("No se pudo realizar la consulta del equipo");
        LimpiarFormulario();
    }
    else {
        $("#dvMensaje").removeClass("alert alert-danger");
        $("#dvMensaje").addClass("alert alert-success");
        $("#dvMensaje").html("");
        //Consultó el equipo
        $("#txtDocumento").val(equipo.CodigoEquipo);
        $("#txtNombre").val(equipo.Nombre);
        $("#txtDescripcion").val(equipo.Descripcion);
        $("#txtCantidad").val(equipo.Cantidad);
    }
}
async function EjecutarComandoEquipo(Metodo, Funcion) {
    let URL = URLBase + "api/Equipos/" + Funcion;
    const equipo = new Equipo($("#txtDocumento").val(), $("#txtNombre").val(), $("#txtDescripcion").val(),
        $("#txtCantidad").val());
    const rpta = await EjecutarComandoServicioAuth(Metodo, URL, equipo);
    LlenarTablaEquipos();
    LimpiarFormulario();
}

function LimpiarFormulario() {
    $("#txtDocumento").val("");
    $("#txtNombre").val("");
    $("#txtDescripcion").val("");
    $("#txtCantidad").val("");
}

async function EjecutarComandoInsertar(Metodo) {
    event.preventDefault();
    let Documento = $("#txtDocumento").val();
    let URL = URLBase + "api/UploadFiles/?Datos=" + Documento + "&Proceso=equipo";
    let archivo = $('#txtSubirImg')[0].files[0];  // <- archivo real
    if (!archivo) {
        $("#dvMensajet").removeClass("alert alert-success");
        $("#dvMensajet").addClass("alert alert-danger");
        $("#dvMensajet").html("Debe seleccionar una imagen.");
        return;
    }

    let formData = new FormData();
    formData.append("Archivo", archivo); // 'Archivo' debe coincidir con el nombre que espera el backend
    formData.append("CodigoEquipo", Documento);

    Token = getCookie("token");

    try {
        let response = await $.ajax({
            url: URL,
            method: Metodo,
            headers: {
                'Authorization': 'Bearer ' + Token
            },
            data: formData,
            processData: false,
            contentType: false,
        });

        LlenarTablaEquipo(); // Actualiza la tabla después de insertar
        LlenarComboImg();
        $("#dvMensajet").removeClass("alert alert-danger");
        $("#dvMensajet").addClass("alert alert-success");
        $("#dvMensajet").html("Imagen subida correctamente");
    } catch (error) {
        $("#dvMensajet").removeClass("alert alert-success");
        $("#dvMensajet").addClass("alert alert-danger");
        $("#dvMensajet").html("No se pudo subir la imagen.");
    }
}
async function EjecutarComandoEliminar(Metodo) {
    event.preventDefault();
    let Documento = $("#cboImagenes").val();
    let URL = URLBase + "api/UploadFiles/?Datos=" + Documento + "&Proceso=equipo";
    let archivo = $('#txtSubirImg')[0].files[0];  // <- archivo real
    if (!archivo) {
        $("#dvMensajet").removeClass("alert alert-success");
        $("#dvMensajet").addClass("alert alert-danger");
        $("#dvMensajet").html("Debe seleccionar una imagen para eliminar.");
        return;
    }

    let formData = new FormData();
    formData.append("Archivo", archivo); // 'Archivo' debe coincidir con el nombre que espera el backend
    formData.append("CodigoEquipo", Documento);

    Token = getCookie("token");

    try {
        let response = await $.ajax({
            url: URL,
            method: Metodo,
            headers: {
                'Authorization': 'Bearer ' + Token
            },
            data: formData,
            processData: false,
            contentType: false,
        });

        LlenarTablaEquipo(); // Actualiza la tabla después de insertar
        LlenarComboImg();
        $("#dvMensajet").removeClass("alert alert-danger");
        $("#dvMensajet").addClass("alert alert-success");
        $("#dvMensajet").html("Se elimino la imagen de la base de datos y en los archivos");
    } catch (error) {
        $("#dvMensajet").removeClass("alert alert-success");
        $("#dvMensajet").addClass("alert alert-danger");
        $("#dvMensajet").html("No se pudo eliminar la imagen.");
    }
}
  
class Equipo {
    constructor(CodigoEquipo, Nombre, Descripcion, Cantidad) {
        this.CodigoEquipo = CodigoEquipo;
        this.Nombre = Nombre;
        this.Descripcion = Descripcion;
        this.Cantidad = Cantidad;
    }
}
class ImagenesEquipo {
    constructor(NombreImagen, CodigoEquipo) {
        this.NombreImagen = NombreImagen;
        this.CodigoEquipo = CodigoEquipo;
    }
}