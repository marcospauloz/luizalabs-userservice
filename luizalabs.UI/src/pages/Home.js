import React, { useState, useEffect } from 'react';
import axios from 'axios';

import Logout from './Logout';

const Home = () => {
  const [username, setUsername] = useState('');

  const handleTestAuthRequest = async () => {
    var id = localStorage.getItem('id');
    
    if (id === null) {
      return;
    }
    
    var testAuthConnection = await axios.get(`http://localhost:8080/Users/${id}`);
    console.log(testAuthConnection);
  };

  useEffect(() => {
    setUsername(localStorage.getItem('name'));

    const timeoutId = setTimeout(handleTestAuthRequest, 1000);
    return () => clearTimeout(timeoutId);

  }, []);

  return (
    <div>
      <div className="LoginPage-title">
        {username ? `Bem vindo a area logada, ${username}!` : 'Bem vindo a area logada!'}
      </div>
      <h3>O objetivo desta página é estar abaixo de uma area logada, onde apenas usuários autenticados conseguem acessar.</h3>
      <Logout />
    </div>
  );
};

export default Home;