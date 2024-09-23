<template>
    <div class="books-component">
        <h1>Books</h1>
        <p>This component demonstrates fetching data from the server.</p>

        <div v-if="loading" class="loading">
            Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
        </div>

        <div v-if="post" class="content">
            <table>
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Author</th>
                        <th>Year</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="book in post" :key="book.id">
                        <td>{{ book.title }}</td>
                        <td>{{ book.authorName }}</td>
                        <td>{{ book.publicationYear }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    type books = {
        id: number,
        title: string,
        authorName: string,
        publicationYear: number,
    }[];

    interface Data {
        loading: boolean,
        post: null | books
    }

    export default defineComponent({
        data(): Data {
            return {
                loading: false,
                post: null
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            fetchData(): void {
                this.post = null;
                this.loading = true;

                fetch('http://localhost:5041'+'/api/Book/')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json as books;
                        this.loading = false;
                        return;
                    });
            }
        },
    });
</script>

<style scoped>
th {
    font-weight: bold;
}

th, td {
    padding-left: .5rem;
    padding-right: .5rem;
}

.books-component {
    text-align: center;
}

table {
    margin-left: auto;
    margin-right: auto;
}
</style>