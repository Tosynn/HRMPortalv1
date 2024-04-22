import React from 'react';
import { NavLink } from 'react-router-dom';

function Navbar() {
  return (
      <>
          <div className="container">
              <h3 className="d-flex justify-content-center m-3">
                  ReactJS App Frontend
              </h3>

              <nav className="navbar navbar-expand-sm bg-light navbar-dark">
                  <ul className="navbar-nav">
                      <li className="nav-item- m-1">
                          <NavLink className="btn btn-light btn-outline-primary" to="/home">
                              Home
                          </NavLink>
                      </li>
                      <li className="nav-item- m-1">
                          <NavLink className="btn btn-light btn-outline-primary" to="/department">
                              Department
                          </NavLink>
                      </li>
                      <li className="nav-item- m-1">
                          <NavLink className="btn btn-light btn-outline-primary" to="/employee">
                              Department
                          </NavLink>
                      </li>
                  </ul>
              </nav>


          </div>
      </>
  );
}

export default Navbar;