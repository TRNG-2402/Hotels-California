import './styles/App.css'
import { Route, Routes } from 'react-router-dom'
import Home from './pages/Home'
import Reservations from './pages/Reservations'
import MemberInvoice from './pages/MemberInvoice'
import Login from './pages/Login'
import Rooms from './pages/Rooms'
import Hotels from './pages/Hotels'
import ManagerViewHotel from './pages/ManagerViewHotel'
import AdminViewAccounts from './pages/AdminViewAccounts'
import NotFound from './pages/NotFound'
import NavBar from './components/NavBar'
import NewHotel from './pages/NewHotel'
import Invoice from './pages/Invoice'
import RoomsOfHotel from './pages/RoomsOfHotel'
import Register from './pages/Register'

function App()
{


  return (
    <>

      <main style={{ flex: 1 }}>
        <h1>Hotels California</h1>
        <p style={{ margin: "32px" }}>Find a home away from home</p>
        <NavBar />
        <Routes>
          <Route path='/' element={<Home />} />
          <Route path='/Login' element={<Login />} />
          <Route path='/Reservations' element={<Reservations />} />
          <Route path='/MemberInvoice' element={<MemberInvoice />} />
          <Route path='/Rooms' element={<Rooms />} />
          <Route path="/hotels/:hotelId/rooms" element={<RoomsOfHotel />} />
          <Route path='/Hotels' element={<Hotels />} />
          <Route path='/NewHotel' element={<NewHotel />} />
          <Route path='/ManagerViewHotel' element={<ManagerViewHotel />} />
          <Route path='/AdminViewAccounts' element={<AdminViewAccounts />} />
          <Route path='/Register' element={<Register />} />
          <Route path='/Invoice' element={<Invoice />} />
          <Route path='/*' element={<NotFound />} />
        </Routes>
      </main>
    </>
  )
}

export default App
