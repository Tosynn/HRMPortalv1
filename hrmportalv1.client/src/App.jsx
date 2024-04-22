import { useEffect, useState } from 'react';
import './App.css';
import { Home } from './pages/Home';
import { Department } from './pages/Department';
import { Employee } from './pages/Employee';
import { Route, createBrowserRouter, createRoutesFromElements, RouterProvider, NavLink } from 'react-router-dom';
import MainLayout from './layout/MainLayout';


function App() {

    const router = createBrowserRouter(
        createRoutesFromElements(
            <Route path='/' element={<MainLayout />}>
                <Route index element={<Home />} />
                <Route path='/Department' element={<Department />} />
                <Route path='/Employee' element={<Employee />} />
            </Route>
        )
    );

    return <RouterProvider router={router}/>
    
}

export default App; 