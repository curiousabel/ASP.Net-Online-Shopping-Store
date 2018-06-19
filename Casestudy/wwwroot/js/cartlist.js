// register modal component
Vue.component("modal", {
    template: "#modal-template",
    props: {
        cart: {},
        details: []
    }
});
// main Vue component
new Vue({
    el: "#carts",
    methods: {
        async getCarts() {
            try {
                this.status = "Loading Carts...";
                response = await axios.get("/GetCarts");
                this.carts = response.data;
                this.status = "";
            } catch (error) {
                console.log(error.statusText);
            }
        },
        async loadAndShowModal() {
            try {
                this.modalStatus = "Loading Details..";
                this.showModal = true;
                response = await axios.get(`/GetCartDetails/${this.CartForModal.id}`);
                this.detailsForModal = response.data;
                this.modalStatus = "";
            } catch (error) {
                console.log(error.statusText);
            }
        }
    },
    mounted() { this.getCarts(); },
    data: {
        carts: [],
        showModal: false,
        CartForModal: {},
        detailsForModal: [],
        status: "",
        modalStatus: ""
    }
});
function formatDate(date) {
    let d;
    // see if date is coming from server
    date === undefined
        ? d = new Date()
        : d = new Date(Date.parse(date)); // date from server
    let _day = d.getDate();
    let _month = d.getMonth() + 1;
    let _year = d.getFullYear();
    let _hour = d.getHours();
    let _min = d.getMinutes();
    if (_min < 10) { _min = "0" + _min; }
    return _year + "-" + _month + "-" + _day + " " + _hour + ":" + _min;
}
