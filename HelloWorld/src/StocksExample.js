import React from 'react';
import dotnetify from 'dotnetify';
import { doesNotReject } from 'assert';

export default class StocksExample extends React.Component {        

    constructor(props) {
        super(props);
        dotnetify.react.connect('StocksHub', this);
        this.state = {
            Greetings: '',
            CurrentParty:'Все',
            CounterParties: [],
            CurrentSymbol:'',
            Symbols: [],
            Entries: []
        };                

        this.handleChange = this.handleChange.bind(this);       
        this.handleChangeSymbol = this.handleChangeSymbol.bind(this);       
    }    

    handleChange(event) {
        this.setState({ CurrentParty: event.target.value });
    }

    handleChangeSymbol(event) {
        this.setState({ CurrentSymbol: event.target.value });
    }

    renderTableData() {        
        return this.state.Entries.map((student, index) => {

            let bgColorStyle = {
                'background-color': 'red'
            }

            const { Source, Volume, EntryTypeString, Price, BackColor } = student //destructuring                        
            
            bgColorStyle["background-color"] = BackColor;

            return (
                <tr style={bgColorStyle} key={index}>
                    <td>{Source}</td>
                    <td>{Volume}</td>
                    <td>{EntryTypeString}</td>
                    <td>{Price}</td>
                </tr>
            )
        })
    }  

    render() {         
        return (                
            <div>
                <h2>Full Book</h2>

                <table>
                    <tr>
                        <td>
                            <select value={this.state.CurrentParty} onChange={this.handleChange}>
                                {this.state.CounterParties.map(val => (<option value={val} key={val}>{val}</option>))}}
                            </select>                    
                        </td>
                        <td>
                            <select value={this.state.CurrentSymbol} onChange={this.handleChangeSymbol}>
                                {this.state.Symbols.map(val => (<option value={val} key={val}>{val}</option>))}}
                            </select>                    
                        </td>
                    </tr>   
                </table>

                <table id="stockTable">
                    <thead>
                        <tr>
                            <th>Источник ликвидности</th>
                            <th>Объем</th>
                            <th>Направление</th>
                            <th>Цена</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.renderTableData()}
                    </tbody>
                </table>
            </div>
        );
    }
}