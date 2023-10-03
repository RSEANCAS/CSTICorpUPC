function inicializarComboAlumno() {
    $('#cmb-alumno').select2({
        theme: 'bootstrap4',
        minimumInputLength: 3,
        ajax: {
            url: '/api/alumno/listarpornombre',
            data: function ({ term }) {
                return {
                    nombres: term
                }
            },
            processResults: function (data, params) {
                data = (data || []).map(x => ({ id: x.AlumnoId, text: `${x.NumeroDocumentoIdentidad} - ${x.Nombres} ${x.Apellidos}`, ...x }));
                return {
                    results: data
                };
            }
        }
    })
}

function listarTipoMatricula() {
    let url = "/api/tipomatricula/listar";

    fetch(url)
        .then(x => x.json())
        .then(cargarComboTipoMatricula);
}

function cargarComboTipoMatricula(data) {
    data = (data || []).map(x => ({ id: x.TipoMatriculaId, text: x.Nombre }));

    $("#cmb-tipomatricula").empty();
    $('#cmb-tipomatricula').select2({
        theme: 'bootstrap4',
        minimumResultsForSearch: Infinity,
        data: [...data]
    })
}

function inicializarComboTipoMatricula() {
    $('#cmb-tipomatricula').select2({
        theme: 'bootstrap4',
        minimumResultsForSearch: Infinity,
        data: []
    })
}

function inicializarComboCurso() {
    $('#cmb-curso').select2({
        theme: 'bootstrap4',
        minimumInputLength: 3,
        ajax: {
            url: '/api/curso/listarpornombre',
            data: function ({ term }) {
                return {
                    descripcion: term
                }
            },
            processResults: function (data, params) {
                data = (data || []).map(x => ({ id: x.CursoId, text: x.Descripcion, ...x }));
                return {
                    results: data
                };
            }
        }
    })
}

function inicializarComboSeccion() {
    $('#cmb-seccion').select2({
        theme: 'bootstrap4',
        minimumResultsForSearch: Infinity,
        data: []
    })
}

function seleccionarCurso() {
    let seccionId = $("#cmb-curso").val();
    let url = `/api/seccion/listarporcurso?cursoId=${seccionId}`;

    fetch(url)
        .then(x => x.json())
        .then(cargarComboSeccion);
}

function cargarComboSeccion(data) {
    data = (data || []).map(x => ({ id: x.SeccionId, text: `${x.Seccion.Nombre} (${x.CantidadVacantesDisponibles} vacantes)`}));

    $("#cmb-seccion").empty();
    $('#cmb-seccion').select2({
        theme: 'bootstrap4',
        minimumResultsForSearch: Infinity,
        data: [...data]
    })
}

function inicializarValidacion() {
    $('#frm-registro').validate({
        rules: {
            alumno: {
                required: true,
                remote: {
                    url: "/api/alumno/existematricula",
                    type: "get",
                    data: {
                        alumno: undefined,
                        alumnoId: () => $("#cmb-alumno").val(),
                        cursoId: () => $("#cmb-curso").val(),
                        seccionId: () => $("#cmb-seccion").val()
                    },
                    dataFilter: function (responseText) {
                        let responseJson = JSON.parse(responseText);
                        return !responseJson;
                    }
                }
            },
            tipomatricula: { required: true },
            curso: { required: true },
            seccion: { required: true },
        },
        messages: {
            alumno: {
                required: "Debe seleccionar una opción",
                remote: "Ya se encuentra matriculado"
            },
            tipomatricula: { required: "Debe seleccionar una opción" },
            curso: { required: "Debe seleccionar una opción" },
            seccion: { required: "Debe seleccionar una opción" },
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        },
        submitHandler: function (form, e) {
            e.preventDefault();
            guardarMatricula();
        }
    });
}

function guardarMatricula() {
    let seccionId = $("#cmb-seccion").val();
    let cursoId = $("#cmb-curso").val();
    let alumnoId = $("#cmb-alumno").val();
    let tipoMatricula = $("#cmb-tipomatricula").val();

    let registro = {};
    registro.SeccionId = seccionId;
    registro.CursoId = cursoId;
    registro.AlumnoId = alumnoId;
    registro.TipoMatriculaId = tipoMatricula;

    let url = "/api/matricula/guardar";
    let init = {};
    init.method = "POST";
    init.headers = {};
    init.headers["Content-Type"] = "application/json";
    init.body = JSON.stringify(registro);

    fetch(url, init)
        .then(x => x.json())
        .then(finalizarGuardado);
}

function finalizarGuardado(data) {
    if (data) {
        alert("Se guardó correctamente");
        location.href = "/matricula";
    }
}

function init() {
    inicializarComboAlumno();
    inicializarComboTipoMatricula();
    listarTipoMatricula();
    inicializarComboCurso();
    inicializarComboSeccion();
    inicializarValidacion();
    $("#cmb-curso").change(seleccionarCurso);
}