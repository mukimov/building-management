class Modal extends React.Component {
	static propTypes = {
		isModalShown: React.PropTypes.bool.isRequired,
		onCloseClick: React.PropTypes.func.isRequired,
		onSaveClick: React.PropTypes.func.isRequired,
		onUpdateClick: React.PropTypes.func.isRequired,
		options: React.PropTypes.arrayOf(React.PropTypes.shape({
			label: React.PropTypes.string,
			value: React.PropTypes.string,
		})),
		expirationDate: React.PropTypes.instanceOf(Date).isRequired,
		isUpdate: React.PropTypes.bool.isRequired,
	};

	componentDidMount() {
		$(this.inputExpirationDate).datepicker({
			dateFormat: "dd.mm.yy",
		});
	}

	componentWillUnmount() {
		$(this.inputExpirationDate).datepicker("destroy");
	}

	handleOnSaveClick = e => {
		e.preventDefault();

		const values = {
			tenantId: this.selectName.value,
			expirationDate: moment(this.inputExpirationDate.value, "DD.MM.YYYY"),
		};
		if (this.props.isUpdate) {
			this.props.onUpdateClick(values);
		} else {
			this.props.onSaveClick(values);
		}
	}

	setSelectNameRef = selectName => {
		this.selectName = selectName;
	};

	setInputExpirationDateRef = inputExpirationDate => {
		this.inputExpirationDate = inputExpirationDate;
	};

	formatDate(date) {
		return moment(date).format("DD.MM.YYYY");
	}

	render() {
		let classNames = `modal ${this.props.isModalShown ? " modal--show" : " fade"}`;
		
		return (
			<div className={classNames} tabindex="-1" role="dialog">
				<div className="modal-dialog" role="document">
					<div className="modal-content">
						<div className="modal-header">
							<button type="button" className="close" onClick={this.props.onCloseClick} aria-label="Close"><span aria-hidden="true">&times;</span></button>
							<h4 className="modal-title">Add tenant</h4>
						</div>
						<div className="modal-body">
							<div className="form-group">
								<label forHtml="selectName">Name</label>
								<select id="selectName" className="form-control" ref={this.setSelectNameRef}>
									{this.props.options.map(o => <option key={o.value} value={o.value}>{o.label}</option>)}
								</select>
							</div>
							<div className="form-group">
								<label forHtml="inputExpirationDate">Expiration date</label>
								<input id="inputExpirationDate" type="text" ref={this.setInputExpirationDateRef} className="form-control" placeholder="Select date"
									defaultValue={this.formatDate(this.props.expirationDate)} />
							</div>
						</div>
						<div className="modal-footer">
							<button type="button" className="btn btn-default" onClick={this.props.onCloseClick}>Close</button>
							<button type="button" className="btn btn-primary" onClick={this.handleOnSaveClick}>{this.props.isUpdate ? "Update" : "Save"} changes</button>
						</div>
					</div>
				</div>
			</div>
		);
	}
}