const Navbar = () => {
  return (
    <nav className="navbar">
      <h1>Vehi Dence</h1>
      <div className="links">
        <a href="/create" style={{ 
          color: 'white', 
          backgroundColor: '#f1356d',
          borderRadius: '8px' 
        }}>My account</a>
      </div>
    </nav>
  );
}
 
export default Navbar;