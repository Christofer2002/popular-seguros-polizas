export class ClienteModel {
    constructor(datos = {}) {
        this.cedulaAsegurado = datos.cedulaAsegurado || ''
        this.nombre = datos.nombre || ''
        this.primerApellido = datos.primerApellido || ''
        this.segundoApellido = datos.segundoApellido || ''
        this.tipoPersona = datos.tipoPersona || ''
        this.fechaNacimiento = datos.fechaNacimiento || ''
    }
}

export class FiltrosClienteModel {
    constructor(datos = {}) {
        this.cedulaAsegurado = datos.cedulaAsegurado || ''
        this.nombre = datos.nombre || ''
        this.primerApellido = datos.primerApellido || ''
        this.segundoApellido = datos.segundoApellido || ''
        this.tipoPersona = datos.tipoPersona || ''
    }
}
