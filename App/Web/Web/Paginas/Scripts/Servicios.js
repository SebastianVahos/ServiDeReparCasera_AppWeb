var URLBase = "http://serv-reparacaseras.runasp.net/";

class Servicio {
    constructor(codigo, nombre, descripcion, precio) {
        this.CodigoServicio = codigo;
        this.Nombre = nombre;
        this.Descripcion = descripcion;
        this.Precio = precio;
    }

    async InsertarServicio() {
        const url = URLBase + "api/Servicios/agregar";
        const dataToInsert = {
            Nombre: this.Nombre,
            Descripcion: this.Descripcion,
            Precio: this.Precio
        };
        return await EjecutarComandoServicioAuth("POST", url, dataToInsert);
    }

    async ActualizarServicio() {
        const url = URLBase + "api/Servicios/actualizar";
        return await EjecutarComandoServicioAuth("PUT", url, this);
    }

    async EliminarServicio() {
        const url = URLBase + "api/Servicios/eliminar?id=" + this.CodigoServicio;
        return await EjecutarComandoServicioAuth("DELETE", url, null);
    }
}

async function EjecutarComandoServicio(Comando) {
    $("#dvMensaje").empty();

    const codigo = $("#txtCodigoServicio").val();
    const nombre = $("#txtNombre").val();
    const descripcion = $("#txtDescripcion").val();
    const precio = parseFloat($("#txtPrecio").val());

    if (!nombre || nombre.trim() === '') {
        $("#dvMensaje").html('<div class="alert alert-danger" role="alert">El campo Nombre del Servicio es obligatorio.</div>');
        return;
    }
    if (!descripcion || descripcion.trim() === '') {
        $("#dvMensaje").html('<div class="alert alert-danger" role="alert">El campo Descripción es obligatorio.</div>');
        return;
    }
    if (isNaN(precio) || precio < 0) {
        $("#dvMensaje").html('<div class="alert alert-danger" role="alert">El Precio debe ser un número válido y positivo.</div>');
        return;
    }

    if ((Comando === 'ACTUALIZAR' || Comando === 'ELIMINAR') && !codigo) {
        $("#dvMensaje").html('<div class="alert alert-danger" role="alert">Para ' + (Comando === 'ACTUALIZAR' ? 'actualizar' : 'eliminar') + ', debe seleccionar un servicio de la tabla.</div>');
        return;
    }

    let servicio = new Servicio(codigo, nombre, descripcion, precio);

    try {
        switch (Comando) {
            case 'GUARDAR':
                await servicio.InsertarServicio();
                break;
            case 'ACTUALIZAR':
                await servicio.ActualizarServicio();
                break;
            case 'ELIMINAR':
                await servicio.EliminarServicio();
                break;
            default:
                console.error("Comando no reconocido:", Comando);
                $("#dvMensaje").html('<div class="alert alert-danger" role="alert">Comando de operación no reconocido.</div>');
                LlenarTablaServicios();
                LimpiarFormularioServicio();
                return;
        }

        LlenarTablaServicios();
        LimpiarFormularioServicio();

    } catch (error) {
        $("#dvMensaje").html('<div class="alert alert-danger" role="alert">Se produjo un error inesperado al realizar la operación: ' + error.message + '</div>');
        console.error("Error en EjecutarComandoServicio:", error);

        LlenarTablaServicios();
        LimpiarFormularioServicio();
    }
}

function LimpiarFormularioServicio() {
    $("#txtCodigoServicio").val("");
    $("#txtNombre").val("");
    $("#txtDescripcion").val("");
    $("#txtPrecio").val("");
    $("#dvMensaje").empty();
}

function EditarServicio(codigo, nombre, descripcion, precio) {
    $("#txtCodigoServicio").val(codigo);
    $("#txtNombre").val(nombre);
    $("#txtDescripcion").val(descripcion);
    $("#txtPrecio").val(precio);
    $("#dvMensaje").empty();
}

async function LlenarTablaServicios() {
    let URL = URLBase + "api/Servicios/ConsultarTodos";

    try {
        const respuesta = await ConsultarServicioAuth(URL);

        if (respuesta && Array.isArray(respuesta)) {
            if ($.fn.DataTable.isDataTable('#tblServicios')) {
                $('#tblServicios').DataTable().destroy();
            }

            $('#tblServicios').DataTable({
                data: respuesta,
                columns: [
                    {
                        data: null,
                        render: function (data, type, row) {
                            return `<button type="button" class="btn btn-info btn-sm" onclick="EditarServicio(
                                '${row.CodigoServicio}',
                                '${escapeHtml(row.Nombre)}',
                                '${escapeHtml(row.Descripcion)}',
                                ${row.Precio}
                            )">Editar</button>`;
                        },
                        title: "Edit",
                        orderable: false,
                        searchable: false
                    },
                    { data: 'CodigoServicio', title: 'Código Servicio' },
                    { data: 'Nombre', title: 'Nombre' },
                    { data: 'Descripcion', title: 'Descripción' },
                    { data: 'Precio', title: 'Precio', render: $.fn.dataTable.render.number(',', '.', 2, '$ ') }
                ],
                language: {
                    url: "//cdn.datatables.net/plug-ins/1.10.25/i18n/Spanish.json"
                },
                responsive: true,
                autoWidth: false,
                destroy: true
            });
        } else {
            $("#dvMensaje").html('<div class="alert alert-warning" role="alert">No se recibieron datos válidos de servicios para mostrar.</div>');
        }
    } catch (error) {
        $("#dvMensaje").html('<div class="alert alert-danger" role="alert">Error al cargar la tabla de servicios: ' + error.message + '</div>');
        console.error("Error al cargar la tabla de servicios:", error);
    }
}

function escapeHtml(text) {
    if (typeof text !== 'string') {
        return text;
    }
    const map = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#039;'
    };
    return text.replace(/[&<>"']/g, function (m) { return map[m]; });
}
jQuery(function () {
    $("#dvMenu").load("../Paginas/Menu.html");
    LlenarTablaServicios();
});