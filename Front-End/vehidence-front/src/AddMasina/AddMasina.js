import React, { useState, useEffect } from "react";
import axios from "axios"; // Importăm Axios
import { useNavigate } from "react-router-dom";
import "./AddMasina.css";

const AddMasina = () => {
  const [serieSasiu, setSerieSasiu] = useState("");
  const [nrInmatriculare, setNrInmatriculare] = useState("");
  const [marca, setMarca] = useState("");
  const [model, setModel] = useState("");
  const [imageFile, setImageFile] = useState(null);
  //const username = localStorage.getItem('username');
  const [id,setId] = useState('');
  const [imageData, setImageData] = useState('');
  const navigate = useNavigate(); // Utilizăm hook-ul useNavigate pentru a naviga între rute
  const email = localStorage.getItem('email');
  const [username,setUsername] = useState('');

  const handleImageChange = (e) => {
    const image = e.target.files[0];
    setImageFile(image);
  };

  useEffect(() => {
    axios
      .get("http://localhost:5277/api/User/All/Users")
      .then((response) => {
        const userData = response.data;
        if (userData.length > 0) {
          const user = userData.find((user) => user.email === email);
          if (user) {
            setUsername(user.username);
            console.log(username);
          }
        }
      })
      .catch((error) => {
        console.error("Error fetching user data:", error);
      });
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault(); // Oprim comportamentul implicit de trimitere a formularului

    // Creăm un obiect FormData pentru a trimite datele către API, inclusiv imaginea
    const masina = new FormData();
    masina.append("SerieSasiu", serieSasiu);
    masina.append("NrInmatriculare", nrInmatriculare);
    masina.append("Marca", marca);
    masina.append("Model", model);
    masina.append("Username", username);
    masina.append("ImageFile", imageFile);
    masina.append("ImageData", imageData);
    masina.append("Id", id);
    try {
      // Facem cererea POST către API-ul "AddMasina" folosind Axios
      const response = await axios.post(
        "http://localhost:5277/api/Masina/AddMasina",
        masina,
        {
          headers: {
            "Content-Type": "multipart/form-data", // Setăm tipul de conținut al cererii ca fiind multipart/form-data
          },
        }
      );
      console.log(response.data); // Poți gestiona răspunsul API-ului aici
      // Dacă dorești, poți redirecționa utilizatorul către o altă pagină după ce a fost adăugată mașina
      if(response.data.statusCode == 200){
        navigate("/home");
      }
      else{
        alert("The car was not added...");
      }
    } catch (error) {
      console.error("Eroare la adăugarea mașinii:", error);
    }
  };

  return (
    <div className="content-add-masina">
      <h1 className="text">New Car</h1>

        <div className="image-upload">
          <div className="image-upload-button">
            <input
              type="file"
              onChange={handleImageChange}
              className="file-input"
            />
            <p className="file-name">{imageFile && imageFile.name}</p>
          </div>
        </div>
        <div className="input-row">
          <div className="inputs-fara-poza">
            <div className="input-fara-poza">
              <input
                type="text"
                placeholder="Car brand"
                value={marca}
                onChange={(e) => setMarca(e.target.value)}
              />
            </div>

            <div className="input-fara-poza">
              <input
                type="text"
                placeholder="Car model"
                value={model}
                onChange={(e) => setModel(e.target.value)}
              />
            </div>
          </div>

          <div className="inputs-fara-poza">
            <div className="input-fara-poza">
              <input
                type="text"
                placeholder="Registration number"
                value={nrInmatriculare}
                onChange={(e) => setNrInmatriculare(e.target.value)}
              />
            </div>
            <div className="input-fara-poza">
              <input
                type="text"
                placeholder="Car chassis number"
                value={serieSasiu}
                onChange={(e) => setSerieSasiu(e.target.value)}
              />
            </div>
          </div>
        </div>
        <button className="button-new-car" onClick={handleSubmit}>
            Add car
        </button>
    </div>
  );
};

export default AddMasina;
