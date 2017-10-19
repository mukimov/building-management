class Tenants extends React.Component {
	static propTypes = {
		initialTenants: React.PropTypes.arrayOf(React.PropTypes.shape({
			name: React.PropTypes.string.isRequired,
			id: React.PropTypes.number.isRequired,
			expirationDate: React.PropTypes.instanceOf(Date),
		})).isRequired,
		updateUrl: React.PropTypes.string.isRequired,
		deleteUrl: React.PropTypes.string.isRequired,
		addUrl: React.PropTypes.string.isRequired,
		getUrl: React.PropTypes.string.isRequired,
		buildingId: React.PropTypes.string.isRequired,
	};

	state = {
		isModalShown: false,
		tenants: this.props.initialTenants.slice(),
		options: [],
		expirationDate: new Date(),
		isUpdate: false,
	};

	handleOnAddClick = e => {
		e.preventDefault();
		$.getJSON(this.props.getUrl).done(result => {
			this.setState({
				options: result.map(t => ({ label: t.name, value: t.id })),
				isModalShown: true,
				isUpdate: false,
			});
		});
	}

	handleOnCloseClick = e => {
		e.preventDefault();
		this.setState({
			isModalShown: false,
		});
	}

	handleOnSaveClick = values => {
		$.ajax({
			type: "POST",
			contentType: "application/json; charset=UTF-8",
			data: JSON.stringify({
				buildingId: this.props.buildingId,
				tenantId: values.tenantId,
				expirationDate: values.expirationDate.format("YYYY-MM-DD HH:mm:ss"),
			}),
			url: this.props.addUrl
		}).done(tenant => {
			this.setState(({ tenants }) => {
				tenants = tenants.slice();
				tenants.push(tenant);

				return {
					tenants,
					isModalShown: false,
				};
			})
		});
	};

	handleOnUpdateClick = values => {
		$.ajax({
			type: "PUT",
			contentType: "application/json; charset=UTF-8",
			data: JSON.stringify({
				buildingId: this.props.buildingId,
				tenantId: values.tenantId,
				expirationDate: values.expirationDate.format("YYYY-MM-DD HH:mm:ss"),
			}),
			url: this.props.updateUrl
		}).done(() => {
			this.setState(({ tenants }) => {
				const tenantIndex = tenants.findIndex(t => t.id === Number(values.tenantId));
				const tenant = tenants[tenantIndex];
				tenants = tenants.slice();
				tenants[tenantIndex] = Object.assign({}, tenant, { expirationDate: values.expirationDate });
				return {
					tenants,
					isModalShown: false,
				};
			})
		});
	};

	handleOnEditClick = item => {
		this.setState({
			options: [
				{
					label: item.name,
					value: item.id,
				}],
			expirationDate: item.expirationDate,
			isModalShown: true,
			isUpdate: true,
		});
	};

	handleOnDeleteClick = item => {
		$.ajax({
			type: "DELETE",
			data: {
				tenantId: item.id,
			},
			contentType: "application/json; charset=UTF-8",
			url: `${this.props.deleteUrl}&tenantId=${item.id}`
		}).done(() => {
			this.setState(({ tenants }) => {
				const tenantIndex = tenants.findIndex(t => t.id === Number(item.tenantId));
				tenants = tenants.slice();
				tenants.splice(tenantIndex, 1);
				return {
					tenants,
				};
			})
		});
	};

	render() {
		return (
			<div className="tenants-container">
				<Modal
					isUpdate={this.state.isUpdate}
					expirationDate={this.state.expirationDate}
					options={this.state.options}
					isModalShown={this.state.isModalShown}
					onSaveClick={this.handleOnSaveClick}
					onUpdateClick={this.handleOnUpdateClick}
					onCloseClick={this.handleOnCloseClick} />
				<table className="table">
					<thead>
						<tr>
							<th>Name</th>
							<th>Expiration date</th>
							<th></th>
						</tr>
					</thead>
					<tbody>
						{this.state.tenants.map(t =>
							<tr key={t.id}>
								<td>{t.name}</td>
								<td>{moment(t.expirationDate).format("DD.MM.YYYY")}</td>
								<td>
									<TenantButton title="Edit" item={t} onClick={this.handleOnEditClick} />
									&nbsp;|&nbsp;
									<TenantButton title="Delete" item={t} onClick={this.handleOnDeleteClick} />
								</td>
							</tr>
						)}
					</tbody>
				</table>
				<a href="#" onClick={this.handleOnAddClick}>Add</a>
			</div>
		);
	}
};

