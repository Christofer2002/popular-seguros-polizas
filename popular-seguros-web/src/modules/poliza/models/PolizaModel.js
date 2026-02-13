export class PolizaModel {
    constructor(datos = {}) {
        this.id = datos.id || ''
        this.numeroPoliza = datos.numeroPoliza || ''
        this.tipoPolizaId = datos.tipoPolizaId || ''
        this.cedulaAsegurado = datos.cedulaAsegurado || ''
        this.montoAsegurado = datos.montoAsegurado || ''
        this.fechaVencimiento = datos.fechaVencimiento || ''
        this.fechaEmision = datos.fechaEmision || ''
        this.tipoCoberturaId = datos.tipoCoberturaId || ''
        this.estadoPolizaId = datos.estadoPolizaId || ''
        this.prima = datos.prima || ''
        this.periodo = datos.periodo || ''
        this.fechaInclusion = datos.fechaInclusion || ''
        this.aseguradora = datos.aseguradora || ''
    }
}

export class FiltrosPolizaModel {
    constructor(datos = {}) {
        this.numeroPoliza = datos.numeroPoliza || ''
        this.tipoPolizaId = datos.tipoPolizaId || ''
        this.fechaVencimiento = datos.fechaVencimiento || ''
        this.cedulaAsegurado = datos.cedulaAsegurado || ''
        this.nombre = datos.nombre || ''
        this.primerApellido = datos.primerApellido || ''
        this.segundoApellido = datos.segundoApellido || ''
    }
}
