import React from "react";
import { SelectInput } from "./base";

export class CountryInput extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      countries: [{ text: "Serbia", value: "RS" }]
    };
  }

  componentDidMount() {
    fetch("./countries.json")
      .then(req => req.json())
      .then(data => {
        this.setState({
          countries: data.map(item => {
            return { text: item.text, value: item.text };
          })
        });
      });
  }

  render() {
    const { countries } = this.state;
    const { name = "countryName", label = "State", ...others } = this.props;
    return (
      <SelectInput name={name} label={label} options={countries} {...others} />
    );
  }
}
