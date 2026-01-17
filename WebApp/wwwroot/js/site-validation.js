var CoreUI = CoreUI || {};

CoreUI.Forms = {
    inicializarMascaras: function () {

        // 1. CÉDULA DE IDENTIDAD (Formato: 000-000000-0000A)
        $('.mask-cedula').mask('000-000000-0000S', {
            translation: {
                'S': { pattern: /[A-Za-z]/ }
            },
            placeholder: "000-000000-0000A",
            onKeyPress: function (value, event) {
                event.currentTarget.value = value.toUpperCase();
            }
        });

        // 2. MONEDA / PRECIOS (Formato: 1,000.00)
        $('.mask-money').mask("#,##0.00", { reverse: true, placeholder: "0,000.00" });

        // 3. SOLO LETRAS SIN ESPACIOS (Útil para nombres de usuario o códigos)
        $('.mask-alpha-no-spaces').on('input', function () {
            this.value = this.value.replace(/[^a-zA-Z]/g, '');
        });

        // 4. PREVENCIÓN DE DOBLE ESPACIO (Limpieza de datos en nombres)
        $('.mask-clean-text').on('input', function () {
            this.value = this.value.replace(/\s\s+/g, ' ');
        });

        // 5. PORCENTAJE (0 - 100)
        $('.mask-percent').mask('##0', {
            onKeyPress: function (val, e, field, options) {
                if (parseInt(val) > 100) field.val('100');
            }
        }); 

        // 1. MÁSCARA TELEFÓNICA (8 dígitos, inicia con 8, 5 o 7)
        $('.mask-phone').mask('Z0000000', {
            translation: {
                'Z': { pattern: /[578]/ } // Solo permite iniciar con 5, 7 u 8
            },
            placeholder: "0000-0000"
        });

        // 2. SOLO LETRAS Y MAYÚSCULAS (Para Nombres)
        $('.mask-caps-only').on('input', function () {
            this.value = this.value.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '').toUpperCase();
        });

        // COMBINADA: Mayúsculas + Solo Letras + Sin Dobles Espacios
        $('.mask-name-clean').on('input', function () {
            let start = this.selectionStart;
            let end = this.selectionEnd;
            let valor = this.value.toUpperCase();
            valor = valor.replace(/[^A-ZÁÉÍÓÚÑ\s]/g, '');
            valor = valor.replace(/\s\s+/g, ' ');
            this.value = valor;
            this.setSelectionRange(start, end);
        });

        // 3. VALIDACIÓN DE CORREO (Formato visual)
        $('.mask-email').on('blur', function () {
            const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (this.value && !regex.test(this.value)) {
                Swal.fire('Formato Inválido', 'Por favor ingrese un correo real', 'error');
                this.value = '';
            }
        });

        // 4. SOLO NÚMEROS
        $('.mask-numbers').on('input', function () {
            this.value = this.value.replace(/[^0-9]/g, '');
        });
    }
};

// Se ejecuta automáticamente al cargar cualquier página
$(function () {
    CoreUI.Forms.inicializarMascaras();
});