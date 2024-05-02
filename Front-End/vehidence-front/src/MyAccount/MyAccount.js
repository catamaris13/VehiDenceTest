import emailIcon from "../assets/email_img.png";
import passwordIcon from "../assets/password_img.png";
import userIcon from "../assets/username_img.png";
import phoneIcon from "../assets/phone_img.png";
import React, { useState, useEffect } from "react";
import '../index.css'
import axios from 'axios';
import { useNavigate } from "react-router-dom";

const MyAccount = () => {
    const history = useNavigate();
    const [name, setName] = useState('');
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [phone, setPhone] = useState('');
    const [token, setToken] = useState('');
    const emailUser = localStorage.getItem('email');
    useEffect(() => {
        axios.get('http://localhost:5277/api/User/All/Users')
            .then(response => {
                const userData = response.data; 
                if (userData.length > 0) {
                    const user = userData.find(user => user.email === emailUser);
                    if(user){
                        setName(user.name);
                        setUsername(user.username);
                        setEmail(user.email);
                        setPassword(user.password);
                        setPhone(user.phoneNo);
                    }
                }
            })
            .catch(error => {
                console.error('Error fetching user data:', error);
            });
    }, []); 

    const handleSave = () => {
        const userData={
            Username:username,
            Password:password,
            Email:email,
            Token:token,
            Name:name,
            PhoneNo:phone
        }
        axios.put('http://localhost:5277/api/User/Update', userData)
        .then(response =>{
            if(response.data.statusCode){
                alert("Your account has been updated successfully!");
                history('/homepage');
            }
            else{
                alert("Error")
            }
        })
        .catch(error => {
            console.error('Failed to save', error);
        })
    }

    const handleDelete = () =>{
        const userData={
            Username:username,
            Password:password,
            Email:email,
            Token:token,
            Name:name,
            PhoneNo:phone
        }
        axios.delete('http://localhost:5277/api/User/Delete', userData)
        .then(response =>{
            if(response.data.statusCode){
                alert("Your account has been deleted!");
                history('/homepage');
            }
            else{
                alert("Error")
            }
        })
        .catch(error => {
            console.error('Failed to delete', error);
        })
    }

    return (
        <div className="login">
            <h1 className="text">Hello {name}</h1>
            <div className="inputs">
                <div className="input">
                    <img src={emailIcon} alt="" />
                    <input
                        type="text"
                        value={email} 
                        readOnly
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </div>

                <div className="input">
                    <img src={userIcon} alt="" />
                    <input
                        type="text"
                        value={username} 
                        onChange={(e) => setUsername(e.target.value)}
                    />
                </div>

                <div className="input">
                    <img src={userIcon} alt="" />
                    <input
                        type="text"
                        value={name} 
                        onChange={(e) => setName(e.target.value)}
                    />
                </div>

                <div className="input">
                    <img src={passwordIcon} alt="" />
                    <input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </div>
                <div className="input">
                    <img src={phoneIcon} alt="" />
                    <input
                        type="text"
                        value={phone} 
                        onChange={(e) => setPhone(e.target.value)}
                    />
                </div>
            </div>
            <div style={{ display: "flex"}}>
                    <button className="button" onClick={handleSave}>Save</button>
                    <button className="button" onClick={handleDelete}>Delete user</button>
            </div>
           

        </div>
    );
}

export default MyAccount;
