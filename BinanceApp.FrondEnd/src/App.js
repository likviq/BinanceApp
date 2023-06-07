import React from 'react';
import { Link, BrowserRouter, Route, Routes } from 'react-router-dom';

import HomePage from './pages/HomePage';
import AboutPage from './pages/AboutPage';
import ConvertPage from './pages/ConvertPage';

import './App.css';

const App = () => {
    return (
        <div>
            <BrowserRouter>
                <nav>
                    <ul className="navbar">
                        <Link to="/">
                            <li>
                                Home
                            </li>
                        </Link>
                        <Link to="/convert">
                            <li>
                                Convert
                            </li>
                        </Link>
                        <Link to="/about">
                            <li>
                                About
                            </li>
                        </Link>
                    </ul>
                </nav>
                <Routes>
                    <Route path="/" element={<HomePage />} />
                    <Route path="/convert" element={<ConvertPage />} />
                    <Route path="/about" element={<AboutPage />} />
                </Routes>
            </BrowserRouter>
        </div>
    );
};

export default App;
