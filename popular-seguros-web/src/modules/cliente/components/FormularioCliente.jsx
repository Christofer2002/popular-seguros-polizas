import React from 'react'
import InputComponent from '../../../components/InputComponent'

const FormularioCliente = ({ 
  cliente, 
  modoEdicion = false,
  soloLectura = false,
  alCambiar, 
  error 
}) => {
  const deshabilitarCedula = modoEdicion || soloLectura
  const deshabilitarCampos = soloLectura

  return (
    <>
      {error && <div className="mensaje-error">{error}</div>}
      <div className="form-grid">
        <InputComponent
          etiqueta="Cédula Asegurado"
          nombre="cedulaAsegurado"
          valor={cliente.cedulaAsegurado}
          alCambiar={alCambiar}
          requerido
          placeholder="Ingrese la cédula"
          maxLength={20}
          clase={deshabilitarCedula ? 'campo-deshabilitado' : ''}
          deshabilitado={deshabilitarCedula}
        />
        <InputComponent
          etiqueta="Nombre"
          nombre="nombre"
          valor={cliente.nombre}
          alCambiar={alCambiar}
          requerido
          placeholder="Ingrese el nombre"
          maxLength={100}
          clase={deshabilitarCampos ? 'campo-deshabilitado' : ''}
          deshabilitado={deshabilitarCampos}
        />
        <InputComponent
          etiqueta="Primer Apellido"
          nombre="primerApellido"
          valor={cliente.primerApellido}
          alCambiar={alCambiar}
          requerido
          placeholder="Ingrese el primer apellido"
          maxLength={100}
          clase={deshabilitarCampos ? 'campo-deshabilitado' : ''}
          deshabilitado={deshabilitarCampos}
        />
        <InputComponent
          etiqueta="Segundo Apellido"
          nombre="segundoApellido"
          valor={cliente.segundoApellido}
          alCambiar={alCambiar}
          placeholder="Ingrese el segundo apellido"
          maxLength={100}
          clase={deshabilitarCampos ? 'campo-deshabilitado' : ''}
          deshabilitado={deshabilitarCampos}
        />
        <InputComponent
          etiqueta="Tipo Persona"
          nombre="tipoPersona"
          valor={cliente.tipoPersona}
          alCambiar={alCambiar}
          requerido
          placeholder="Ej: Física, Jurídica"
          maxLength={20}
          clase={deshabilitarCampos ? 'campo-deshabilitado' : ''}
          deshabilitado={deshabilitarCampos}
        />
        <InputComponent
          etiqueta="Fecha Nacimiento"
          nombre="fechaNacimiento"
          tipo="date"
          valor={cliente.fechaNacimiento}
          alCambiar={alCambiar}
          requerido
          clase={deshabilitarCampos ? 'campo-deshabilitado' : ''}
          deshabilitado={deshabilitarCampos}
        />
      </div>
    </>
  )
}

export default FormularioCliente
