import './App.css'
import { Route, Routes } from 'react-router-dom'
import Home from './pages/Home'
import Reservations from './pages/Reservations'
import MemberInvoice from './pages/MemberInvoice'
import LogIn from './pages/LogIn'
import Rooms from './pages/Rooms'
import Hotels from './pages/Hotels'
import ManagerViewHotel from './pages/ManagerViewHotel'
import AdminViewAccounts from './pages/AdminViewAccounts'
import AdminViewHotels from './pages/AdminViewHotels'
import NotFound from './pages/NotFound'

function App() {


  return (
    <main>
      <h1>Hotels California</h1>
      <p>Find a home away from home</p>

      <Routes>
        <Route path='/' element={<Home />} />
        <Route path='/Login' element={<LogIn />} />
        <Route path='/Reservations' element={<Reservations />} />
        <Route path='/MemberInvoice' element={<MemberInvoice />} />
        <Route path='/Rooms' element={<Rooms />} />
        <Route path='/Hotels' element={<Hotels />} />
        <Route path='/ManagerViewHotel' element={<ManagerViewHotel />} />
        <Route path='/AdminViewAccounts' element={<AdminViewAccounts />} />
        <Route path='/AdminViewHotels' element={<AdminViewHotels />} />
        <Route path='/*' element={<NotFound />} />
      </Routes>
    </main>
  )
}

export default App
