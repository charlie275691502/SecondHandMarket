import axios from 'axios'

const BASE_URL = 'https://secondhandmarket-iskc.onrender.com'

const api = axios.create({ baseURL: BASE_URL })

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token')
  if (token) config.headers.Authorization = `Bearer ${token}`
  return config
})

// Auth
export const register = (data) => api.post('/api/auth/register', data)
export const login = (data) => api.post('/api/auth/login', data)

// Listings
export const getListings = (params) => api.get('/api/listings', { params })
export const createListing = (data) => api.post('/api/listings', data)
