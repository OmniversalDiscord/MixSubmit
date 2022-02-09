import { useRef, useState, useCallback, useEffect } from "react";
import { getErrorMessage } from "./errors";

import { useDropzone } from "react-dropzone";

function App() {
  const [error, setError] = useState<string | null>(null);
  const [errorShake, setErrorShake] = useState<boolean>(true);

  const onDrop = useCallback((acceptedFiles, rejectedFiles) => {
    // If there are any rejected files, an invalid file was dropped.
    if (rejectedFiles.length > 0) {
      setError(`error: file must be an mp3. ${getErrorMessage()}`);
      setErrorShake(true);
      return;
    } else if (acceptedFiles.length > 1) {
      setError(`error: more than one file was selected. ${getErrorMessage()}`);
      setErrorShake(true);
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

  // Disable the shake animation after 0.5 seconds
  useEffect(() => {
    if (errorShake) {
      const timer = setTimeout(() => {
        setErrorShake(false);
      }, 100);
      return () => clearTimeout(timer);
    }
  }, [errorShake]);

  return (
    <div
      className="w-screen h-screen p-8"
      style={{ backgroundColor: "#222831" }}
    >
      <div
        className="drop-zone border-white border-opacity-50 rounded-xl border-2 w-full h-full flex items-center justify-center cursor-pointer"
        {...getRootProps()}
      >
        <div
          className={`flex flex-col items-center space-y-2 select-none ${
            errorShake && "shake-horizontal shake-constant"
          }`}
        >
          <h1 className="text-white text-5xl opacity-90 font-bold">
            submit your mix
          </h1>
          <h2 className="text-white text-xl opacity-50">
            drag an mp3 here or click to upload
          </h2>

          {error != null && (
            <h2 className="text-red-500 text-xl opacity-70">{error}</h2>
          )}
        </div>
      </div>
      <input {...getInputProps()} accept=".mp3" />
    </div>
  );
}

export default App;
