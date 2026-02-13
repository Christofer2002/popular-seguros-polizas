import React from 'react'
import InputComponent from '../../../components/InputComponent'

const FiltrosCliente = ({
  filtros,
  alCambiar
}) => {
  return (
    <>
      <InputComponent
        etiqueta="Cédula Asegurado"
        nombre="cedulaAsegurado"
        valor={filtros.cedulaAsegurado}
        alCambiar={alCambiar}
        placeholder="Ingrese cédula"
      />
      <InputComponent
        etiqueta="Nombre"
        nombre="nombre"
        valor={filtros.nombre}
        alCambiar={alCambiar}
        placeholder="Ingrese nombre"
      />
      <InputComponent
        etiqueta="Primer Apellido"
        nombre="primerApellido"
        valor={filtros.primerApellido}
        alCambiar={alCambiar}
        placeholder="Ingrese primer apellido"
      />
      <InputComponent
        etiqueta="Segundo Apellido"
        nombre="segundoApellido"
        valor={filtros.segundoApellido}
        alCambiar={alCambiar}
        placeholder="Ingrese segundo apellido"
      />
      <InputComponent
        etiqueta="Tipo Persona"
        nombre="tipoPersona"
        valor={filtros.tipoPersona}
        alCambiar={alCambiar}
        placeholder="Ingrese tipo persona"
      />
    </>
  )
}

export default FiltrosCliente
