import React,{useState} from "react";
import axios from "axios";
import{ useNavigate} from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.min.css';


function Registration(){
  const history=useNavigate();
    const [Username,setUsername]=useState('')
    const [Email,setEmail]=useState('')
    const [Password,setPassword]=useState('')
    

    const handleSave=(e) =>{
        e.preventDefault();
        if(Username===''||Email===''||Password===''){
          alert("All 3 fields are mandatory")
        }
        else 
        {
           const url=`https://localhost:7165/api/User/Registration`;
        const data={
            Username:Username,
            Email:Email,
            Password:Password,
            
        }
        console.log(url)

        axios.post(url,data)
        .then((result)=>{
          clear()
            const dt=result.data;
            alert(dt.statusMessage);
            history("/");

        })
        .catch((error)=>{
            console.log(error);
        })
        }
       
    }

    const handleLogin=(e)=>{
      history("/");
    }

    const clear =()=>{
      setUsername('');
      setEmail('');
      setPassword('');
      
      
    }

    return (
        <section class="vh-100 gradient-custom">
  <div class="container py-5 h-100">
    <div class="row justify-content-center align-items-center h-100">
      <div class="col-12 col-lg-9 col-xl-7">
        <div class="card shadow-2-strong card-registration" style={{'border-radius': '15px'}}>
          <div class="card-body p-4 p-md-5">
            <h3 class="mb-4 pb-2 pb-md-0 mb-md-5">Registration Form</h3>
            <form>

              <div class="row">
                <div class="col-md-6 mb-4">

                  <div class="form-outline">
                    <input type="text"
                     id="firstName"
                      class="form-control form-control-lg"
                      onChange={(e)=>setUsername(e.target.value)} 
                      value={Username}
                      />
                    <label class="form-label" for="firstName">Username</label>
                  </div>

                </div>
                <div class="col-md-6 mb-4">

                  <div class="form-outline">
                    <input type="email" 
                    id="emailAddress" 
                    class="form-control form-control-lg" 
                    onChange={(e)=>setEmail(e.target.value)}
                    value={Email}/>
                    <label class="form-label" for="emailAddress">Email</label>
                  </div>

                </div>
               
              </div>

              

              <div class="row">
               
                
                <div class="col-md mb-4 pb-2">

                  <div class="form-outline">
                    <input type="Password" 
                    id="Password" 
                    class="form-control form-control-lg" 
                    onChange={(e)=>setPassword(e.target.value)}
                    value={Password}
                    />
                    <label class="form-label" for="Password">Password</label>
                  </div>

                </div>
              </div>

              

              <div class="d-flex justify-content-end pt-3">
                <button type="button" className="btn btn-light btn-lg"
                onClick={(e)=> clear()}>
                    Reset all
                </button>
                <button
                 type="button" 
                 className="btn btn-warning btn_lg ms-2"
                 onClick={(e)=> handleSave(e)}
                 >
                    Submit form
                </button>
                <button
                 type="button" 
                 className="btn btn-warning btn_lg ms-2"
                 onClick={(e)=> handleLogin(e)}
                 >
                    Login
                </button>
                
                
              </div>

            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</section>
    )
}

export default Registration;