import React, { useState, useEffect } from 'react';
import axios from 'axios';

const HomePage = () => {
    const [topCurrencies, setTopCurrencies] = useState([]);

    useEffect(() => {
        const fetchTopCurrencies = async () => {
            try {
                const response = await axios.get('https://localhost:7170/api/general/topcurrencies');
                setTopCurrencies(response.data);
            } catch (error) {
                console.error('Error fetching top currencies:', error);
            }
        };

        fetchTopCurrencies();
    }, []);

    return (
        <div>
            <h1>Home Page</h1>
            <p>This is the home page.</p>
            <h2>Top Currencies</h2>
            <ul>
                {topCurrencies.map((currency) => (
                    <li key={currency.symbol}>{currency.symbol} {currency.volume}</li>
                ))}
            </ul>
        </div>
    );
};

export default HomePage;