import React from 'react'
import InputComponent from '../../../components/InputComponent'
import SelectComponent from '../../../components/SelectComponent'

const FiltrosPoliza = ({
  filtros,
  alCambiar,
  tiposPoliza = []
}) => {
  return (
    <>
      <InputComponent
        etiqueta="N° Póliza"
        nombre="numeroPoliza"
        valor={filtros.numeroPoliza}
        alCambiar={alCambiar}
        placeholder="Ingrese número de póliza"
      />
      <SelectComponent
        etiqueta="Tipo de Póliza"
        nombre="tipoPolizaId"
        valor={filtros.tipoPolizaId}
        alCambiar={alCambiar}
        opciones={tiposPoliza}
        placeholder="Seleccione tipo..."
      />
      <InputComponent
        etiqueta="Fecha Vencimiento"
        nombre="fechaVencimiento"
        tipo="date"
        valor={filtros.fechaVencimiento}
        alCambiar={alCambiar}
      />
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
    </>
  )
}

export default FiltrosPoliza
