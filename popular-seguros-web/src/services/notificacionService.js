import { toast } from 'react-toastify'

const opcionesBase = {
  position: 'top-right',
  autoClose: 3000,
  hideProgressBar: false,
  closeOnClick: true,
  pauseOnHover: true,
  draggable: true
}

export const notificacionExito = (mensaje) => {
  toast.success(mensaje, {
    ...opcionesBase,
    className: 'toast-exito'
  })
}

export const notificacionInfo = (mensaje) => {
  toast.info(mensaje, {
    ...opcionesBase,
    className: 'toast-info'
  })
}

export const notificacionAdvertencia = (mensaje) => {
  toast.warning(mensaje, {
    ...opcionesBase,
    className: 'toast-advertencia'
  })
}

// Para errores menores que no requieren modal
export const notificacionError = (mensaje) => {
  toast.error(mensaje, {
    ...opcionesBase,
    autoClose: 5000,
    className: 'toast-error'
  })
}
