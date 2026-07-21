const LoginPage = () => {
  return (
    <div className="flex flex-col flex-1 justify-center items-center">
      <div className="flex flex-col border border-black-900 rounded border-2">
        <span className="text-center text-3xl font-bold w-120 mt-5">Login</span>
        <input
          type="text"
          placeholder="Username or Email"
          className="text-center border border-black mt-25 mx-5 text-lg p-1 rounded"
        />
        <input 
          type="password"
          placeholder="Password"
          className="text-center border border-black mt-3 mx-5 text-lg p-1 rounded"
        />
        <button className="mx-5 mt-15 mb-10 bg-gray-500 rounded text-xl p-1
                          text-white cursor-pointer hover:opacity-75 active:opacity-25">LOG IN</button>
      </div>
    </div>
  );
};

export default LoginPage;