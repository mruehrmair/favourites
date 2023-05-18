const root = 'api/bookmarks';

export default {
    loadAll: () => `${root}`,
    //   removeArticleFromOrderList: (articleId: string) => `${root}/${articleId}`,
    addBookmark: () => `${root}`
    //   changeArticleNumber: (articleId: string) => `${root}/${articleId}/articleNumber`,
    //   changeArticleDescription: (articleId: string) => `${root}/${articleId}/description`,
    //   changeArticleManufacturer: (articleId: string) => `${root}/${articleId}/manufacturer`,
    //   changeArticleQuantity: (articleId: string) => `${root}/${articleId}/quantity`,
};