import { useRef, useState, useCallback, useEffect } from "react";
import { getErrorMessage } from "./errors";

import { useDropzone } from "react-dropzone";
import { useParams } from "react-router-dom";

import bg from '../assets/bg.jpg';
import note from '../assets/music-solid.svg';
import logo from '../assets/omni.png';

function App() {
  const [error, setError] = useState<string | null>(null);
  const [errorShake, setErrorShake] = useState<boolean>(false);

  const { mixId } = useParams();

  const showError = (error: string) => {
    setError(`error: ${error}. ${getErrorMessage()}`);
    setErrorShake(true);
  };

  const onDrop = useCallback((acceptedFiles, rejectedFiles) => {
    // If there are any rejected files, an invalid file was dropped.
    if (rejectedFiles.length > 0) {
      showError("file must be an mp3");
      return;
    }

    if (acceptedFiles.length > 1) {
      showError("only one file can be uploaded");
      return;
    }

    // Log the file name
    console.log(acceptedFiles[0].name);
    setError(null);
  }, []);

  const { getRootProps, getInputProps } = useDropzone({
    onDrop,
    accept: "audio/mpeg",
  });

  // Disable the shake animation after 100 ms
  useEffect(() => {
    if (errorShake) {
      const timer = setTimeout(() => {
        setErrorShake(false);
      }, 100);
      return () => clearTimeout(timer);
    }
  }, [errorShake]);

  return (
    <>
      <div
        className="w-screen h-screen bg-cover bg-center bg-no-repeat fixed"
        style={{ backgroundImage: `url(${bg})` }}
      >
      </div>
      <div className="fixed px-4 py-4 flex items-center space-x-4">
        <img src={logo} className="h-6" />
      </div>
      <div className="w-screen h-screen flex items-center justify-center">
        <div className="bg-white rounded-xl flex flex-col p-3 drop-shadow-xl">
          <div
            className="drop-zone cursor-pointer w-72 h-72 border-2 rounded-lg border-black border-opacity-30 flex items-center justify-center flex-col"
            {...getRootProps()}>

            <img src={note} className="w-16 h-16 opacity-30" />

            <h1 className="text-xl font-extrabold text-black opacity-70 mt-4">
              Upload your mix
            </h1>
            <h2 className="text-sm opacity-40 font-medium">
              Drop an mp3 or click to upload
            </h2>
          </div>
          <input {...getInputProps()} accept=".mp3" />
        </div>
      </div>
    </>
  );
}

export default App;
