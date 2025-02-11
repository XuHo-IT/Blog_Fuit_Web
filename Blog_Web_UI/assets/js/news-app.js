const news = {templagte:`<h1>News section</h1>`,

  data() {
    return {
      news: [],
    };
  },
  methods: {
    async refreshData() {
      try {
        const response = await axios.get(
          "https://localhost:7208/api/Food/GetNews"
        );
        this.news = response.data;
        console.log("Data fetched successfully!");
      } catch (error) {
        console.error("Error fetching news data:", error.message);
      }
    },
  },
  mounted() {
    this.refreshData();
  }
}
