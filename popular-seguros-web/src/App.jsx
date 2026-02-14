import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'
import { ToastContainer } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'
import Login from './pages/Login'
import Cliente from './modules/cliente/Cliente'
import Poliza from './modules/poliza/Poliza'

const App = () => {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/popular-seguros-polizas/web/" element={<Login />} />
          <Route path="/popular-seguros-polizas/web/clientes" element={<Cliente />} />
          <Route path="/popular-seguros-polizas/web/polizas" element={<Poliza />} />
          <Route path="/" element={<Navigate to="/popular-seguros-polizas/web/" replace />} />
        </Routes>
      </BrowserRouter>
      <ToastContainer
        position="top-right"
        autoClose={3000}
        hideProgressBar={false}
        newestOnTop
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="colored"
      />
    </>
  )
}

export default App
