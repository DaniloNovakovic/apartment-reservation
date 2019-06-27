import React from "react";
import { SelectInput } from "../../baseFormHelpers";
import { countriesService } from "../../../services";

export class CountryInput extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      countries: [{ text: "Loading...", value: "RS" }]
    };
  }

  componentDidMount() {
    const { required } = this.props;
    countriesService.getCountries().then(data => {
      let countries = data.map(item => {
        return { text: item.text, value: item.value };
      });

      if (!required) {
        countries = [{ text: "", value: "" }, ...countries];
      }

      this.setState({ countries });
    });
  }

  render() {
    const { countries } = this.state;
    let {
      name = "countryName",
      label = "State",
      value = "",
      ...others
    } = this.props;
    return (
      <SelectInput
        name={name}
        label={label}
        value={value.toUpperCase()}
        options={countries}
        {...others}
      />
    );
  }
}
