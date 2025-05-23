using AutoMapper;

namespace HandmadeMarket.DTO.CartDTOs
{
    public class CartProfile:Profile
    {
        public CartProfile() 
        {
            CreateMap<Cart, CartGetAllDTO>().ReverseMap();
            CreateMap<Cart, CartWithProductDTO>().ReverseMap();
            CreateMap<Cart, UpdateCartDTO>().ReverseMap();


        }
    }
}
