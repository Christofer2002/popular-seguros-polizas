import React from 'react'
import InputComponent from '../../../components/InputComponent'
import SelectComponent from '../../../components/SelectComponent'

const FormularioPoliza = ({ 
  poliza, 
  alCambiar, 
  tiposPoliza = [],
  estadosPoliza = [],
  coberturas = [],
  error 
}) => {
  return (
    <>
      {error && <div className="mensaje-error">{error}</div>}
      <div className="form-grid">
        <InputComponent
          etiqueta="Número Póliza"
          nombre="numeroPoliza"
          valor={poliza.numeroPoliza}
          alCambiar={alCambiar}
          requerido
          placeholder="Ej: POL-001"
          maxLength={20}
        />
        <SelectComponent
          etiqueta="Tipo Póliza"
          nombre="tipoPolizaId"
          valor={poliza.tipoPolizaId}
          alCambiar={alCambiar}
          opciones={tiposPoliza}
          requerido
        />
        <InputComponent
          etiqueta="Cédula Asegurado"
          nombre="cedulaAsegurado"
          valor={poliza.cedulaAsegurado}
          alCambiar={alCambiar}
          requerido
          placeholder="Ingrese la cédula"
          maxLength={20}
        />
        <InputComponent
          etiqueta="Monto Asegurado"
          nombre="montoAsegurado"
          tipo="number"
          valor={poliza.montoAsegurado}
          alCambiar={alCambiar}
          requerido
          placeholder="0.00"
        />
        <InputComponent
          etiqueta="Fecha Emisión"
          nombre="fechaEmision"
          tipo="date"
          valor={poliza.fechaEmision}
          alCambiar={alCambiar}
          requerido
        />
        <InputComponent
          etiqueta="Fecha Vencimiento"
          nombre="fechaVencimiento"
          tipo="date"
          valor={poliza.fechaVencimiento}
          alCambiar={alCambiar}
          requerido
        />
        <SelectComponent
          etiqueta="Cobertura"
          nombre="tipoCoberturaId"
          valor={poliza.tipoCoberturaId}
          alCambiar={alCambiar}
          opciones={coberturas}
          requerido
        />
        <SelectComponent
          etiqueta="Estado Póliza"
          nombre="estadoPolizaId"
          valor={poliza.estadoPolizaId}
          alCambiar={alCambiar}
          opciones={estadosPoliza}
          requerido
        />
        <InputComponent
          etiqueta="Prima"
          nombre="prima"
          tipo="number"
          valor={poliza.prima}
          alCambiar={alCambiar}
          requerido
          placeholder="0.00"
        />
        <InputComponent
          etiqueta="Periodo"
          nombre="periodo"
          tipo="date"
          valor={poliza.periodo}
          alCambiar={alCambiar}
          requerido
        />
        <InputComponent
          etiqueta="Fecha Inclusión"
          nombre="fechaInclusion"
          tipo="date"
          valor={poliza.fechaInclusion}
          alCambiar={alCambiar}
          requerido
        />
        <InputComponent
          etiqueta="Aseguradora"
          nombre="aseguradora"
          valor={poliza.aseguradora}
          alCambiar={alCambiar}
          requerido
          placeholder="Nombre de la aseguradora"
          maxLength={100}
        />
      </div>
    </>
  )
}

export default FormularioPoliza
