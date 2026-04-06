import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import Navbar from './components/Navbar'
import ProtectedRoute from './components/ProtectedRoute'
import LoginPage from './pages/LoginPage'
import RegisterPage from './pages/RegisterPage'
import ListingsPage from './pages/ListingsPage'
import CreateListingPage from './pages/CreateListingPage'

const queryClient = new QueryClient()

export default function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Navbar />
        <Routes>
          <Route path="/" element={<Navigate to="/listings" replace />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/listings" element={<ListingsPage />} />
          <Route
            path="/create-listing"
            element={
              <ProtectedRoute>
                <CreateListingPage />
              </ProtectedRoute>
            }
          />
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  )
}
