import Button from "./Button";

const movies = [
    {title: 'The Green Mile', description: 'Moćima', id:6},  
    {title: 'Inception', description: 'Snova', id:7},  
    {title: 'The Notebook', description: 'Priča ', id:8},  
    {title: 'MovieX', description: 'Opis', id:9},  
    {title: 'MovieY', description: 'Opis', id:10}  

]


export default function Grid({isPacked}){
    const listItems = movies.map(movie=>
        <>
        
        <tbody>
            <tr>
                <td>{movie.title}</td>
                <td>{movie.description}</td>               
                <td>
                <Button button={'update'}/>
                <Button button={'delete'}/>
                </td>
            </tr>
        </tbody>
        
        </>
    )
    return(
        <table className="movie-table">
            <thead> 
            <tr>
                <th>Title</th>
                <th>Description</th>
                <th>Actions</th>
            </tr> 
            </thead>
            {listItems} 
        </table>
    )
}