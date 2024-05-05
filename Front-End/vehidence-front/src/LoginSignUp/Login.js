import emailIcon from "../assets/email_img.png";
import passwordIcon from "../assets/password_img.png";
import React, { useState, useContext } from "react";
import '../index.css'
import axios from 'axios';
import{ useNavigate} from "react-router-dom";

const Login = () => {
    const  history=useNavigate();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [name, setName] = useState('');
    const [username, setUsername] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [token, setToken] = useState('');
    const [id, setId] = useState('');
    const [isValid, setisVaid] = useState('');
    const [isLogin, setIsLogin] = useState(false);
    const handleLogin = (e)=>{
        
        e.preventDefault();
        
        const data ={
          Username:username,
          Password:password,
          Email:email,
          Token:token,
          Name:name,
          PhoneNo:phoneNumber
        }
        const url='http://localhost:5277/api/User/Login';
        axios.post(url,data)
        .then((result)=>{
            const dt=result.data;
            
            if(dt.statusCode===200)
            {
                setIsLogin(true);
                localStorage.setItem('islogin',true);
               // localStorage.setItem('email',email);
                history("/home");
                //window.location.href('/signup')
            }
            else 
            {
                if(dt.statusCode===100)
                alert(dt.statusMessage);
            }
            
          })
          
        .catch((error)=>{
            console.log(error);
        })
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
