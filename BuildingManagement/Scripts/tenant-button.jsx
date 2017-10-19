class TenantButton extends React.Component {
	static propTypes = {
		onClick: React.PropTypes.func.isRequired,
		item: React.PropTypes.shape({
			name: React.PropTypes.string.isRequired,
			expirationDate: React.PropTypes.instanceOf(Date),
		}),
		title: React.PropTypes.string.isRequired,
	}

	handleOnClick = e => {
		e.preventDefault();

		this.props.onClick(this.props.item);
	}

	render() {
		const { title } = this.props;
		return <a href="#" onClick={this.handleOnClick}>{title}</a>;
	}
}