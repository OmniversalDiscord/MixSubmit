import { useRef, useState } from 'react'
import { getErrorMessage } from './errors'

function App() {
  const mixFile = useRef<HTMLInputElement | null>(null)

  const onDropperClick = () => {
    mixFile.current?.click();
  }

  return (
    <div className="w-screen h-screen p-8" style={{backgroundColor: "#222831"}}>
      <div className="drop-zone border-white border-opacity-50 rounded-xl border-2 w-full h-full flex items-center justify-center cursor-pointer" onClick={onDropperClick}>
        <div className="flex flex-col items-center space-y-2 select-none">
          <h1 className="text-white text-5xl opacity-90 font-bold">submit your mix</h1>
          <h2 className="text-white text-xl opacity-50">drag an mp3 here or click to upload</h2>

          <h2 className="text-red-500 text-xl opacity-70">error: file was not an mp3. {getErrorMessage()}</h2>
        </div>
      </div>
      <input type="file" ref={mixFile} className="hidden" accept=".mp3" />
    </div>
  )
}

export default App
