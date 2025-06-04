var URLBase = "http://serv-reparacaseras.runasp.net/";

let serviciosDisponibles = [];
let clientesDisponibles = [];

class Programacion {
    constructor(idProgramacion, fecha, hora, direccion, estado, documento, codigoServicio) {
        this.IdProgramacion = idProgramacion;
        this.Fecha = fecha;
        this.Hora = hora;
        this.Direccion = direccion;
        this.Estado = estado;
        this.Documento = documento;
        this.CodigoServicio = codigoServicio;
    }

    async InsertarProgramacion() {
        const url = URLBase + "api/programaciones/agregar";
        const dataToInsert = {
            Fecha: this.Fecha,
            Hora: this.Hora,
            Direccion: this.Direccion,
            Estado: this.Estado,
            Documento: this.Documento,
            CodigoServicio: this.CodigoServicio
        };
        return await EjecutarComandoServicioAuth("POST", url, dataToInsert);
    }

    async ActualizarProgramacion() {
        const url = URLBase + "api/programaciones/actualizar";
        return await EjecutarComandoServicioAuth("PUT", url, this);
    }

    async EliminarProgramacion() {
        const url = URLBase + "api/programaciones/eliminar?id=" + this.IdProgramacion;
        return await EjecutarComandoServicioAuth("DELETE", url, null);
    }
}

async function EjecutarComandoProgramacion(Comando) {
    $("#dvMensaje").empty();

    const idProgramacion = $("#txtIdProgramacion").val();
    const fecha = $("#txtFecha").val();
    const hora = $("#txtHora").val();
    const direccion = $("#txtDireccion").val();
    const estado = $("#cboEstado").val();
    const documento = $("#cboDocumento").val();
    const codigoServicio = $("#cboCodigoServicio").val();

    if (!fecha || !hora || !direccion || direccion.trim() === '' || !estado || estado.trim() === '' || !documento || !codigoServicio) {
        $("#dvMensaje").html('<div class="alert alert-danger" role="alert">Todos los campos (Fecha, Hora, Dirección, Estado, Cliente, Servicio) son obligatorios.</div>');
        return;
    }

    if ((Comando === 'ACTUALIZAR' || Comando === 'ELIMINAR') && !idProgramacion) {
        $("#dvMensaje").html('<div class="alert alert-danger" role="alert">Para ' + (Comando === 'ACTUALIZAR' ? 'actualizar' : 'eliminar') + ', debe seleccionar una programación de la tabla.</div>');
        return;
    }

    let programacion = new Programacion(idProgramacion, fecha, hora, direccion, estado, documento, codigoServicio);
    let operacionRealizada = false;

    try {
        switch (Comando) {
            case 'GUARDAR':
                await programacion.InsertarProgramacion();
                operacionRealizada = true;
                break;
            case 'ACTUALIZAR':
                await programacion.ActualizarProgramacion();
                operacionRealizada = true;
                break;
            case 'ELIMINAR':
                await programacion.EliminarProgramacion();
                operacionRealizada = true;
                break;
            default:
                console.error("Comando no reconocido:", Comando);
                $("#dvMensaje").html('<div class="alert alert-danger" role="alert">Comando de operación no reconocido.</div>');
                return;
        }
        LimpiarFormularioProgramacion();

    } catch (error) {
        $("#dvMensaje").html('<div class="alert alert-danger" role="alert">Se produjo un error inesperado al realizar la operación: ' + error.message + '</div>');
        console.error("Error en EjecutarComandoProgramacion:", error);
        operacionRealizada = true;
    } finally {
        if (operacionRealizada) {
            LlenarTablaProgramaciones();
        }
    }
}

function LimpiarFormularioProgramacion() {
    $("#txtIdProgramacion").val("");
    $("#txtFecha").val("");
    $("#txtHora").val("");
    $("#txtDireccion").val("");
    $("#cboEstado").val("");
    $("#cboDocumento").val("");
    $("#cboCodigoServicio").val("");
    $("#dvMensaje").empty();
}

function EditarProgramacion(idProgramacion, fecha, hora, direccion, estado, documento, codigoServicio) {
    $("#txtIdProgramacion").val(idProgramacion);
    $("#txtFecha").val(moment(fecha).format('YYYY-MM-DD'));
    $("#txtHora").val(moment(hora, 'HH:mm:ss').format('HH:mm'));
    $("#txtDireccion").val(direccion);
    $("#cboEstado").val(estado);
    $("#cboDocumento").val(documento);
    $("#cboCodigoServicio").val(codigoServicio);
    $("#dvMensaje").empty();
}

function obtenerNombreServicio(codigo) {
    const servicio = serviciosDisponibles.find(s => s.CodigoServicio == codigo);
    return servicio ? servicio.Nombre : 'Desconocido';
}

function obtenerNombreCliente(documento) {
    const cliente = clientesDisponibles.find(c => c.Documento == documento);
    return cliente ? cliente.NombreCompleto : 'Desconocido';
}

async function LlenarTablaProgramaciones() {
    let URL = URLBase + "api/programaciones/consultarTodos";

    try {
        const respuesta = await ConsultarServicioAuth(URL);

        if (respuesta && Array.isArray(respuesta)) {
            if ($.fn.DataTable.isDataTable('#tblProgramaciones')) {
                $('#tblProgramaciones').DataTable().destroy();
            }

            $('#tblProgramaciones').DataTable({
                data: respuesta,
                columns: [
                    {
                        data: null,
                        render: function (data, type, row) {
                            const formattedFecha = moment(row.Fecha).format('YYYY-MM-DD');
                            const formattedHora = moment(row.Hora, 'HH:mm:ss').format('HH:mm');
                            return `<button type="button" class="btn btn-info btn-sm" onclick="EditarProgramacion(
                                '${row.IdProgramacion}',
                                '${escapeHtml(formattedFecha)}',
                                '${escapeHtml(formattedHora)}',
                                '${escapeHtml(row.Direccion)}',
                                '${escapeHtml(row.Estado)}', 
                                '${escapeHtml(row.Documento)}',
                                '${escapeHtml(row.CodigoServicio)}'
                            )">Editar</button>`;
                        },
                        title: "Edit",
                        orderable: false,
                        searchable: false
                    },
                    { data: 'IdProgramacion', title: 'ID Programación' },
                    {
                        data: 'Fecha',
                        title: 'Fecha',
                        render: function (data) {
                            return moment(data).format('YYYY-MM-DD');
                        }
                    },
                    {
                        data: 'Hora',
                        title: 'Hora',
                        render: function (data) {
                            return moment(data, 'HH:mm:ss').format('HH:mm');
                        }
                    },
                    { data: 'Direccion', title: 'Dirección' },
                    { data: 'Estado', title: 'Estado' },
                    { data: 'Documento', title: 'Documento Cliente' },
                    {
                        data: 'Documento',
                        title: 'Nombre Cliente',
                        render: function (data, type, row) {
                            return escapeHtml(obtenerNombreCliente(data));
                        }
                    },
                    {
                        data: 'CodigoServicio',
                        title: 'Servicio',
                        render: function (data, type, row) {
                            return escapeHtml(obtenerNombreServicio(data));
                        }
                    }
                ],
                language: {
                    url: "//cdn.datatables.net/plug-ins/1.10.25/i18n/Spanish.json"
                },
                responsive: true,
                autoWidth: false,
                destroy: true
            });
        } else {
            $("#dvMensaje").html('<div class="alert alert-warning" role="alert">No se recibieron datos válidos de programaciones para mostrar.</div>');
        }
    } catch (error) {
        $("#dvMensaje").html('<div class="alert alert-danger" role="alert">Error al cargar la tabla de programaciones: ' + error.message + '</div>');
        console.error("Error al cargar la tabla de programaciones:", error);
    }
}

async function CargarCombosProgramacion() {
    try {
        const clientesUrl = URLBase + "api/Clientes/ConsultarTodos";
        const clientes = await ConsultarServicioAuth(clientesUrl);
        if (clientes && Array.isArray(clientes)) {
            clientesDisponibles = clientes;
            $("#cboDocumento").empty().append('<option value="">Seleccione un cliente</option>');
            clientes.forEach(cliente => {
                $("#cboDocumento").append(new Option(cliente.NombreCompleto + " (" + cliente.Documento + ")", cliente.Documento));
            });
        } else {
            console.warn("No se pudieron cargar los clientes.");
        }

        const serviciosUrl = URLBase + "api/Servicios/ConsultarTodos";
        const servicios = await ConsultarServicioAuth(serviciosUrl);
        if (servicios && Array.isArray(servicios)) {
            serviciosDisponibles = servicios;
            $("#cboCodigoServicio").empty().append('<option value="">Seleccione un servicio</option>');
            servicios.forEach(servicio => {
                $("#cboCodigoServicio").append(new Option(servicio.Nombre + " (" + servicio.CodigoServicio + ")", servicio.CodigoServicio));
            });
        } else {
            console.warn("No se pudieron cargar los servicios.");
        }

    } catch (error) {
        console.error("Error al cargar los combos de Programación:", error);
        $("#dvMensaje").html('<div class="alert alert-danger" role="alert">Error al cargar las opciones de clientes/servicios.</div>');
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

    $('#dtpFecha').datetimepicker({
        format: 'YYYY-MM-DD'
    });
    $('#dtpHora').datetimepicker({
        format: 'HH:mm'
    });

    CargarCombosProgramacion().then(() => {
        LlenarTablaProgramaciones();
    }).catch(error => {
        console.error("Error en la carga inicial de combos o tabla:", error);
        $("#dvMensaje").html('<div class="alert alert-danger" role="alert">Error al iniciar la página: No se pudieron cargar los datos esenciales.</div>');
    });
});