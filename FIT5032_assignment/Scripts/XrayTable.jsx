class XrayTypes extends React.Component {
    constructor(props) {
        super(props);
        this.state = { data: [], highlightedIndex: -1 };
    }

    componentWillMount() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', '/xraystype', true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ data: data });
        };
        xhr.send();
    }

    highlightMostExpensive = () => {
        const { data } = this.state;

        if (data.length === 0) return;

        let maxPrice = data[0].referenceMaxPrice;
        let maxIndex = 0;

        for (let i = 1; i < data.length; i++) {
            if (data[i].referenceMaxPrice > maxPrice) {
                maxPrice = data[i].referenceMaxPrice;
                maxIndex = i;
            }
        }

        this.setState({ highlightedIndex: maxIndex });
    };

    render() {
        const { data, highlightedIndex } = this.state;

        return (
            <div>
                <h2 className="text-2xl font-bold mb-4">Xray Types</h2>
                <button
                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded mb-10"
                    onClick={this.highlightMostExpensive}
                >
                    Highlight Most Expensive
                </button>
                <ul>
                    {data.map((xrayType, index) => (
                        <li
                            key={index}
                            className={
                                `border p-2 mb-2 ${highlightedIndex === index ? 'bg-yellow-200' : ''}`
                            }
                        >
                            <strong>Name:</strong> {xrayType.name}<br />
                            <strong>Min Price:</strong> {xrayType.referenceMinPrice}<br />
                            <strong>Max Price:</strong> {xrayType.referenceMaxPrice}
                        </li>
                    ))}
                </ul>
            </div>
        );
    }
}

ReactDOM.render(<XrayTypes />, document.getElementById('content'));
