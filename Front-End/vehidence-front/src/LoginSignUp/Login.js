import emailIcon from "../assets/email_img.png";
import passwordIcon from "../assets/password_img.png";
import React, { useState } from "react";
import '../index.css'

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [name, setName] = useState('');
    const [username, setUsername] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [token, setToken] = useState('');
    const [id, setId] = useState('');
    const [isValid, setisVaid] = useState('');
    const handleLogin = async() => {
        try {
            const response = await fetch('http://localhost:5277/api/User/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({id, name, password, email, username, phoneNumber, isValid, token}),

            })
            const responseData = await response.json(); 
        
        if (response.ok) {

            if (responseData.statusCode === 200) {
                window.location.href = '/home';
            } else {
                console.error('Login failed:', responseData.statusMessage);
            }

        } else {
            
            console.error('HTTP error:', response.statusText);
        }
    } catch (error) {
        console.error('Login error:', error);
    }
    }

    return (
        <div className="login">
            <h1 className="text">Login</h1>
            <div className="inputs">
                <div className="input">
                    <img src={emailIcon} alt="" />
                    <input
                        type="text"
                        placeholder="Email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </div>

                <div className="input">
                    <img src={passwordIcon} alt="" />
                    <input
                        type="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </div>
            </div>
            <button className="button" onClick={handleLogin}>Login</button>
        </div>
    );
};

export default Login;
