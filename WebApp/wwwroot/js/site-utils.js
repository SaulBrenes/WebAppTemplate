var CoreUI = CoreUI || {};

CoreUI.Selectores = {
    configurarBusqueda: function (config) {
        const {
            btnId,
            sourceId,   // [[id][id1, id2]
            targetId,
            url,
            placeholder,
            obtenerDatosCustom
        } = config;

        $(`#${btnId}`).on('click', function () {
            const selectDestino = $(`#${targetId}`);
            const boton = $(this);
            let datosParaEnviar = {};

            if (obtenerDatosCustom && typeof obtenerDatosCustom === "function") {
                datosParaEnviar = obtenerDatosCustom();
            } else {
                const valorOrigen = $(`#${config.sourceId}`).val();
                if (!valorOrigen) {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Atención',
                        text: 'Por favor, seleccione una opción para buscar',
                        confirmButtonColor: '#3085d6'
                    });
                    return;
                }
                datosParaEnviar[config.paramName] = valorOrigen;
            }

            const contenidoOriginal = boton.html();
            boton.prop("disabled", true).html('<span class="spinner-border spinner-border-sm"></span>');
            selectDestino.empty().append(`<option value="">-- Cargando... --</option>`);

            $.getJSON(url, datosParaEnviar)
                .done(function (data) {
                    selectDestino.empty();
                    if (data && data.length > 0) {
                        selectDestino.append(`<option value="">-- ${placeholder} (${data.length}) --</option>`);
                        $.each(data, function (i, item) {
                            selectDestino.append($('<option>', { value: item.llave, text: item.valor }));
                        });
                    } else {
                        selectDestino.append(`<option value="">-- Sin resultados --</option>`);
                        // Alerta de "No hay datos"
                        Swal.fire({
                            icon: 'info',
                            title: 'Sin resultados',
                            text: 'No se encontraron registros que coincidan con la búsqueda.',
                            timer: 3000,
                            showConfirmButton: false
                        });
                    }
                })
                .fail(() => {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Hubo un fallo al conectar con el servidor.'
                    });
                    selectDestino.empty().append(`<option value="">Error de servidor</option>`);
                })
                .always(() => boton.prop("disabled", false).html(contenidoOriginal));
        });
    }
};