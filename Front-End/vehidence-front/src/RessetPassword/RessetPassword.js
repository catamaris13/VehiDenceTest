import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import passwordIcon from '../assets/password_img.png';

const RessetPassword = () => {

    const [email, setEmail] = useState('');
    const [name, setName] = useState('');
    const [username, setUsername] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const navigate = useNavigate();
    const location = useLocation();
    const [loading, setLoading] = useState(true);
    const [validationSuccess, setValidationSuccess] = useState(false);
    const [password1, setPassword1] = useState('');
    const [password2, setPassword2] = useState('');

    const handleHomePage = () => {
        navigate('/home');
    };

    const returnToHome = () =>{
        navigate('/home');
    }

    const handleSave = () => {
        const searchParams = new URLSearchParams(location.search);
         const token = searchParams.get('token');

        if (password1 !== password2) {
            alert('Passwords do not match');
            return;
        }

        const data ={
            Username:username,
            Password:password1,
            Email:email,
            Token:token,
            Name:name,
            PhoneNo:phoneNumber
        }
        axios.post('http://localhost:5277/api/User/ResetPassword', data)
            .then(response => {
                if(response.data.statusCode == 200){
                    console.log(response.data);
                    alert('Password reset successful');
                    handleHomePage();
                }
                else{
                    alert("Error in setting the user password");
                }
                
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Failed to reset password');
            });
    };

    useEffect(() => {
        const searchParams = new URLSearchParams(location.search);
        const token = searchParams.get('token');
        
        setValidationSuccess(false);
    
        if (token) {
            axios.get(`http://localhost:5277/api/User/ValidareLinkParola?token=${token}`)
                .then(response => {
                    if(response.data.statusCode == 200){
                        console.log(response.data);
                        setValidationSuccess(true); 
                    }
                    else {
                        setValidationSuccess(false);
                    }
                    
                })
                .catch(error => {
                    console.error('Error:', error);
                    setValidationSuccess(false);
                })
                .finally(() => {
                    setLoading(false); 
                });
        } else {

            setLoading(false);
        }
    }, [location.search]);
    

    if(validationSuccess == true){
        return (
            <div className="login">
                <h1 className="text">Reset Password</h1>
                <div className="inputs">
                    <div className="input">
                        <img src={passwordIcon} alt="" />
                        <input
                            type="password"
                            placeholder="Password"
                            value={password1}
                            onChange={(e) => setPassword1(e.target.value)}
                        />
                    </div>

                    <div className="input">
                        <img src={passwordIcon} alt="" />
                        <input
                            type="password"
                            placeholder="Confirm Password"
                            value={password2}
                            onChange={(e) => setPassword2(e.target.value)}
                        />
                    </div>
                </div>
                <button className="button" onClick={handleSave}>Save</button>
            </div>
        );
    }
    else {
        return (
            <div className="login">
                <h1 className="text">Page error</h1>
                <button className="button" onClick={returnToHome}>Return to home</button>
            </div>
        );
    }
    
};

export default RessetPassword;
