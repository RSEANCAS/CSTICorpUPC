function inicializarListaResultados() {
    $("#tbl-lista").DataTable({
        processing: true,
        serverSide: true,
        order: [[0, 'desc']],
        ajax: {
            url: `/api/matricula/listar`,
            data: function (params, settings) {
                let [columnIndex, sortOrder] = $(settings.nTable).DataTable().order()[0];
                let sortName = params.columns[columnIndex].name || params.columns[columnIndex].data;

                let { start, length } = params;

                params.nombresAlumno = $("#txt-filtro-nombresalumno").val();
                params.columns = undefined;
                params.length = undefined;
                params.order = undefined;
                params.search = undefined;
                params.start = undefined;

                params.pageNumber = (start / length) + 1;
                params.pageSize = length;
                params.sortName = sortName;
                params.sortOrder = sortOrder;
            },
            //dataFilter: function (responseText) {
            //    let responseJson = JSON.parse(responseText);
            //    let jsonString = 
            //}
        },
        columns: [
            { name: "MatriculaId", data: "MatriculaId" },
            { name: "AlumnoCodigo", data: "Alumno.Codigo" },
            { name: "AlumnoNombres", data: "Alumno.Nombres" },
            { name: "AlumnoApellidos", data: "Alumno.Apellidos" },
            { name: "CursoId", data: "CursoId" },
            { name: "CursoDescripcion", data: "Curso.Descripcion" },
            { name: "CursoCantidadCreditos", data: "Curso.CantidadCreditos" },
            { name: "SeccionNombre", data: "Seccion.Nombre" },
            { name: "TipoMatriculaNombre", data: "TipoMatricula.Nombre" },
            { name: "FechaMatricula", data: "FechaMatricula", render: (data) => moment(data).format("DD/MM/YYYY HH:mm") },
            { name: "FechaAnulado", data: "FechaAnulado", render: (data) => data == null ? null : moment(data).format("DD/MM/YYYY HH:mm") }
        ],
        paging: true,
        lengthChange: false,
        searching: false,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true,
    })
}


function buscar() {
    $("#tbl-lista").DataTable().ajax.reload();
}

function init() {
    $("#btn-buscar").click(buscar);
    inicializarListaResultados();
}