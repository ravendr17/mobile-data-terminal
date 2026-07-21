import React, { useState } from "react";

const LoginPage = () => {
  const [usernameOrEmail, setUsernameOrEmail] = useState("");
  const [password, setPassword] = useState("");
  const [usernameOrEmailError, setUsernameOrEmailError] = useState("");
  const [passwordError, setPasswordError] = useState("");

  const login = () => {
    if (usernameOrEmail.trim().length === 0) {
      setUsernameOrEmailError("Username or email required.");
    }
    if (password.trim().length === 0) {
      setPasswordError("Password required.");
    }
  }

  return (
    <div className="flex flex-col flex-1 justify-center items-center">
      <div className="flex flex-col border border-black-900 rounded border-2">
        <span className="text-center text-3xl font-bold w-120 mt-5">Login</span>
        <span className="mt-25 ml-5 text-red-500"> 
          {usernameOrEmailError || "\u00A0"}
        </span>
        <input
          type="text"
          placeholder="Username or Email"
          className="text-center border border-black mx-5 text-lg p-1 rounded"
          value={usernameOrEmail}
          onChange={(e) => {
            setUsernameOrEmail(e.target.value);
            setUsernameOrEmailError("");
          }}
        />
        <span className="mt-2 ml-5 text-red-500">
          {passwordError || "\u00A0"}
        </span>
        <input 
          type="password"
          placeholder="Password"
          className="text-center border border-black mx-5 text-lg p-1 rounded"
          value={password}
          onChange={(e) => {
            setPassword(e.target.value);
            setPasswordError("");
          }}
        /> 
        <button className="mx-5 mt-20 mb-10 bg-green-600 rounded text-xl p-1
                          text-white cursor-pointer hover:opacity-75 active:opacity-25"
                onClick={login}
        >LOG IN
        </button>
      </div>
    </div>
  );
};

export default LoginPage;