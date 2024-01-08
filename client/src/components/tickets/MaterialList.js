import { useEffect, useState } from "react";
import { Table } from "reactstrap";
import { getMaterials, deleteMaterial } from "../../data/materialsData";
import { Link } from "react-router-dom";
import { getGenres } from "../../data/genresData"
import { getMaterialTypes } from "../../data/materialTypesData"

export default function MaterialList() {
  const [materials, setMaterials] = useState([]);
  const [genres, setGenres] = useState([])
  const [materialTypes, setMaterialTypes] = useState([])
  const [chosenGenre, setChosenGenre] = useState(0)
  const [chosenMaterialType, setChosenMaterialType] = useState(0)
  const [filteredMaterials, setFilteredMaterials] = useState([])

  // useEffect(() => {
  //   getMaterials().then(setMaterials)
  // }, []);

  const getAndSetMaterials = () => {
    getMaterials().then(setMaterials)
  }

  useEffect(() => {
    getAndSetMaterials()
  }, [])

  const handleDeleteBtn = (event, id) => {
    event.preventDefault()
    deleteMaterial(id).then(() => getAndSetMaterials())
  }

  useEffect(() => {
    getGenres().then(array => setGenres(array))
  }, [])

  useEffect(() => {
    getMaterialTypes().then(array => setMaterialTypes(array))
  }, [])

  useEffect(() => {
    if (chosenGenre === 0) {
      setFilteredMaterials(materials);
    } else {
      const foundMaterials = materials.filter((m) => m.genreId === parseInt(chosenGenre));
      setFilteredMaterials(foundMaterials);
    }
  }, [chosenGenre, materials]);

  useEffect(() => {
    if (chosenMaterialType === 0) {
      setFilteredMaterials(materials);
    } else {
      const foundMaterials = materials.filter((m) => m.materialTypeId === parseInt(chosenMaterialType));
      setFilteredMaterials(foundMaterials);
    }
  }, [chosenMaterialType, materials]);


  return (
    <div className="container">
      <div className="sub-menu bg-light">
        <h4>Materials</h4>
        <Link to="/materials/create">Add</Link>
      </div>
      <div>
        Filter by Genre:
        <select
          value={chosenGenre}
          onChange={(event) => setChosenGenre(event.target.value)} >
          <option value="0">Genre Options</option>
          {genres.map((g) => {
            return (
              <option value={g.id} key={g.id}>{g?.name}</option>
            )
          })}
        </select>
      </div>
      <div>
        Filter by Material Type:
        <select
          value={chosenMaterialType}
          onChange={(event) => {
            setChosenMaterialType(event.target.value)
          }}
        >
          <option value="0">Material Type Options</option>
          {materialTypes.map((m) => {
            return (
              <option value={m.id} key={m.id}>{m.name}</option>
            )
          })}
        </select>
      </div>
      <Table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Title</th>
            <th>Type</th>
            <th>Genre</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {materials.map((m) => (
            <tr key={`materials-${m.id}`}>
              <th scope="row">{m.id}</th>
              <td>{m.materialName}</td>
              <td>{m.materialType.name}</td>
              <td>{m.genre.name}</td>
              <td>
                <Link to={`${m.id}`}>Details</Link>
              </td>
              <td><button onClick={event => handleDeleteBtn(event, m.id)}>Delete Material</button></td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
}
